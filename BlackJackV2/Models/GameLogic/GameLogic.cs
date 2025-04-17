using Avalonia.Media.Imaging;
using BlackJackV2.Constants;
using BlackJackV2.Models.CardDeck;
using BlackJackV2.Models.CardFactory;
using BlackJackV2.Models.CardHand;
using BlackJackV2.Models.Player;
using BlackJackV2.ViewModels;
using BlackJackV2.Services.Messaging;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Subjects;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using BlackJackV2.Services.Events;

namespace BlackJackV2.Models.GameLogic
{
	public class GameLogic
	{
		// Subject to notify if players in game change
		public Subject<Dictionary<string, IPlayer>> PlayerChangedEvent { get; }

		// Used to notify when the bet value is updated
		public Subject<BetUpdateEvent> BetUpdateEvent { get; }

		// Subject to notify when the bet is requested
		public Subject<IPlayer> BetRequestedEvent { get; }

		public Subject<SplitSuccessfulEvent> splitSuccessfulEvent { get; } 

		// Subject and IObservable to notify when the game state changes
		private BehaviorSubject<GameState> _gameStateSubject = new BehaviorSubject<GameState>(new GameState());
		public IObservable<GameState> GameStateObservable => _gameStateSubject.AsObservable();


		// Used to wait for specific player bet input to be received
		private Dictionary<string, TaskCompletionSource<int>> _betInputTask; 


		// Used to create a deck of cards
		private BlackJackCardDeck blackJackCardDeck;
		// Handles the blackjack related actions the players can take
		private PlayerAction playerAction;
		// Handles the dealer's turn in a blackjack game
		private DealerLogic dealerLogic;
		// Handles the evaluation of the round
		private RoundEvaluator roundEvaluator;
		// Handles all rounds related to a players hands
		public PlayerRound playerRound;

		// A collection of players in the game
		public Dictionary<string, IPlayer> Players { get; }

		// Represents the player and dealer hands
		IPlayerHands<Bitmap, string> _playerCardHand;
		IPlayerHands<Bitmap, string> _dealerCardHand;
		public IPlayerHands<Bitmap, string> PlayerCardHand { get => _playerCardHand; }
		public IPlayerHands<Bitmap, string> DealerCardHand { get => _dealerCardHand; }



		public GameLogic()
		{
			PlayerChangedEvent = new Subject<Dictionary<string, IPlayer>>();
			BetUpdateEvent = new Subject<BetUpdateEvent>();
			BetRequestedEvent = new Subject<IPlayer>();
			splitSuccessfulEvent = new Subject<SplitSuccessfulEvent>();

			_betInputTask = new Dictionary<string, TaskCompletionSource<int>>();

			blackJackCardDeck = (BlackJackCardDeck)BlackJackCardDeckCreator.CreateBlackJackCardDeck();
			_dealerCardHand = BlackJackPlayerHandsCreator.CreateBlackJackPlayerHand(HandOwners.HandOwner.Dealer);

			// As of now this is a single player game so we will only have one player
			Players = new Dictionary<string, IPlayer>();

			playerAction = GameLogicCreator.CreatePlayerAction(splitSuccessfulEvent);
			dealerLogic = GameLogicCreator.CreateDealerLogic();
			roundEvaluator = GameLogicCreator.CreateRoundEvaluator();
			playerRound = GameLogicCreator.CreatePlayerRound(playerAction, splitSuccessfulEvent);

			
		}

		// Updates the FundsAndBet state and notify subscribers
		// Action<> means "a method that takes a FundsAndBet state and modifies it, but doesn't return anything.
		private void UpdateGameState(Action<GameState> updateAction)
		{
			// Specify the subject to update
			var newState = _gameStateSubject.Value;
			
			// Update the state using the provided action
			updateAction(newState);

			// Notify subscribers about the new state
			_gameStateSubject.OnNext(newState);
		}

		public async void InitiateNewRound()
		{
			foreach (KeyValuePair<string, IPlayer> player in Players)
			{
				
				string playerName = player.Key;
				IPlayer currentPlayer = player.Value;

				// Adds a new completion source for the bet input task
				TaskCompletionSource<int> betInputTask = new TaskCompletionSource<int>();
				_betInputTask[playerName] = betInputTask;

				// Notify which player is to place their bet
				BetRequestedEvent.OnNext(currentPlayer);

				// Change the state to show that logic is waiting for the bet input
				UpdateGameState(state => state.IsBetRecieved = false);

				// Wait for the bet input to be received
				int betInput = await betInputTask.Task;
				
				// Change the state to show that logic is waiting for the bet input
				UpdateGameState(state => state.IsBetRecieved = true);

				Debug.WriteLine($"Bet input received for {playerName}: {betInput}");
			}

			Debug.WriteLine("All bet inputs received. Starting new round.");
		}
		
		public async void StartNewRound()
		{
			blackJackCardDeck.ShuffleDeck();

			// Gives dealer his initial cards
			dealerLogic.InitialDeal(DealerCardHand, blackJackCardDeck);

			foreach (KeyValuePair<string, IPlayer> player in Players)
			{
				// Player conducts their turn
				await playerRound.PlayerTurn(blackJackCardDeck, player.Value);

			}
		
			// Dealer finishes his turn
			dealerLogic.DealerFinishTurn(DealerCardHand, blackJackCardDeck);
			
			EvaluateRound();
		}



		private void EvaluateRound()
		{
			// Evaluate the round and determine the winner
			Debug.WriteLine(roundEvaluator.EvaluateRound(PlayerCardHand.PrimaryCardHand, DealerCardHand.PrimaryCardHand));
		}

		private void EvaluateSingleHand(BlackJackCardHand cardHand) 
		{
			
		}

		// This method is called when the player inputs their bet
		// Completes the betUpdateCompletionSource task for specified player
		public void OnBetInputReceived(string playerName, int betInput)
		{
			if (_betInputTask.TryGetValue(playerName, out var betUpdateCompletionSource))
			{
				Players[playerName].PlaceBet(HandOwners.HandOwner.Primary, betInput);
				betUpdateCompletionSource.SetResult(betInput);
				_betInputTask.Remove(playerName);
			}
		}

		// This method is called when the player changes
		// Notify subscribers about the player changes
		public void OnPlayerChangedReceived(List<string> playerNames) 
		{
			Players.Clear();

			foreach (string playerName in playerNames)
			{
				Players.Add(playerName,
					BlackJackPlayerHandsCreator.CreatePlayer(
						BlackJackPlayerHandsCreator.CreateBlackJackPlayerHand( HandOwners.HandOwner.Player ), 
						BetUpdateEvent, 
						playerName
					)
				);
			}

			PlayerChangedEvent.OnNext(Players);
		}
	}
}
