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
		//// Represents the current active hand in the game
		//HandOwners.HandOwner activeHand;

		// Contains and notyfies about the funds and bet of the player
		private BehaviorSubject<GameState> _gameStateSubject = new BehaviorSubject<GameState>(new GameState());
		public IObservable<GameState> GameStateObservable => _gameStateSubject.AsObservable();


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

		// Represents the player and dealer hands
		IPlayerHands<Bitmap, string> _playerCardHand;
		IPlayerHands<Bitmap, string> _dealerCardHand;
		public IPlayerHands<Bitmap, string> PlayerCardHand { get => _playerCardHand; }
		public IPlayerHands<Bitmap, string> DealerCardHand { get => _dealerCardHand; }


		// Used to wait for the bet input to be received
		private TaskCompletionSource<bool> _taskBetInputReceived;

		// Used to notify when the bet value is updated
		public Subject<BetUpdateEvent> BetUpdateEvent { get; } 

		public GameLogic()
		{
			blackJackCardDeck = (BlackJackCardDeck) BlackJackCardDeckCreator.CreateBlackJackCardDeck();
			BetUpdateEvent = new Subject<BetUpdateEvent>();
			_playerCardHand = BlackJackPlayerHandsCreator.CreateBlackJackPlayerHand(BetUpdateEvent, HandOwners.HandOwner.Player);
			_dealerCardHand = BlackJackPlayerHandsCreator.CreateBlackJackPlayerHand(BetUpdateEvent, HandOwners.HandOwner.Dealer);

			playerAction = GameLogicCreator.CreatePlayerAction();
			dealerLogic = GameLogicCreator.CreateDealerLogic();
			roundEvaluator = GameLogicCreator.CreateRoundEvaluator();
			playerRound = GameLogicCreator.CreatePlayerRound(blackJackCardDeck, playerAction);

			_taskBetInputReceived = new TaskCompletionSource<bool>();
			


			UpdateGameState(state =>
			{
				state.Points = 10; // Set initial points
			});
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
			// Change the state to show that logic is waiting for the bet input
			UpdateGameState( state => state.IsBetRecieved = false );

			// Wait for the bet input to be received (statsViewModel)
			await _taskBetInputReceived.Task;

			// Once the bet input is received, update the state to show that the bet has been received
			UpdateGameState(state => state.IsBetRecieved = true);

			Debug.WriteLine("Bet input received.");
		}
		
		public async void StartNewRound()
		{
			blackJackCardDeck.ShuffleDeck();

			// Gives dealer his initial cards
			dealerLogic.InitialDeal(DealerCardHand, blackJackCardDeck);

			// Player conducts their turn
			await playerRound.PlayerTurn((PlayerHands)_playerCardHand);

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
		// It sets the bet input and updates the game state, and completes the _taskBetInputReceived task
		public void OnBetInputReceived(int betInput)
		{
			_taskBetInputReceived.SetResult(true);
			// Set the bet for the primary hand
			_playerCardHand.SetBetToHand(HandOwners.HandOwner.Primary, betInput);

			// Deduct the bet from the points
			UpdateGameState(state => state.Points -= betInput);
		}


	}
}
