// Project: BlackJackV2
// file: BlackJackV2/Models/GameLogic/GameCoordinator.cs


using BlackJackV2.Factories.CardDeckFactory;
using BlackJackV2.Factories.CardHandFactory;
using BlackJackV2.Factories.PlayerFactory;
using BlackJackV2.Factories.PlayerHandsFactory;
using BlackJackV2.Models.CardDeck;
using BlackJackV2.Models.CardHand;
using BlackJackV2.Models.GameLogic.Dealer_Services;
using BlackJackV2.Models.GameLogic.GameRuleServices;
using BlackJackV2.Models.GameLogic.PlayerServices;
using BlackJackV2.Models.Player;
using BlackJackV2.Models.PlayerHands;
using BlackJackV2.Services.Events;
using BlackJackV2.Shared.Constants;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;



/// <summary>
///		This is a Facade used to simplefy game logic classes and functionality  
///		
///		public		Subject<Dictionary<string, IPlayer>>		PlayerChangedEvent		:	Subject to notify if players in game change
///		public		Subject<BetUpdateEvent>						BetUpdateEvent			:	Used to notify when the bet value is updated
///		public		Subject<IPlayer>							BetRequestedEvent		:	Subject to notify when the bet is requested
///		public		Subject<SplitSuccessfulEvent>				splitSuccessfulEvent	:	Subject to notify when the player split is successful
///		public		IObservable<GameState>						GameStateObservable		:	Subject and IObservable to notify when the game state changes
///		
///		public		IBlackJackPlayerHands<Bitmap, string>		DealerCardHand			:	Represents the dealers hands	
///  
/// 	async Task		RegisterBetForNewRound();								: Initiates and wait for player bets retrival
///		void			OnBetInputReceived(string playerName, int betInput)		: Called when the player inputs their bet	
///		async Task		StartNewRound()											: Starts a new round of the game, handles player turns and dealer's turn
///		void			EvaluateRound()											: Evaluates the round and determines the winner
///		void			OnPlayerChangedReceived(List<string> playerNames)		: Called when the current players are changed
/// </summary>


namespace BlackJackV2.Models.GameLogic
{
	public class GameCoordinator<TImage, TValue> : IGameCoordinator<TImage, TValue>
	{
		// Subject to notify if players in game change
		public Subject<Dictionary<string, IPlayer<TImage, TValue>>> PlayerChangedEvent { get; }

		// Used to notify when the bet value is updated
		public Subject<BetUpdateEvent> BetUpdateEvent { get; }

		// Subject to notify when the bet is requested
		public Subject<IPlayer<TImage, TValue>> BetRequestedEvent { get; }

	
		// Subject and IObservable to notify when the game state changes
		private BehaviorSubject<GameState> _gameStateSubject = new BehaviorSubject<GameState>(new GameState());
		public IObservable<GameState> GameStateObservable => _gameStateSubject.AsObservable();


		// Used to wait for specific player bet input to be received
		private Dictionary<string, TaskCompletionSource<int>> _betInputTask;

		// Used to create a deck of cards
		private ICardDeck<TImage, TValue> _cardDeck;
	
		private IDealerServices<TImage,TValue> _dealerServices;
		private PlayerServices<TImage, TValue> _playerServices;
		private GameRuleServices<TImage, TValue> _gameRuleServices;

		// A collection of players in the game
		public Dictionary<string, IPlayer<TImage, TValue>> Players { get; }

		
		// These objects will be removed when the coordinator is implemented
		BlackJackCardHandCreator<TImage, TValue> _cardHandCreator;
		BlackJackPlayerHandsCreator<TImage, TValue> _playerCardHandsCreator;
		BlackJackPlayerCreator<TImage, TValue> _playerCreator;


		public GameCoordinator(
				BlackJackCardDeckCreator<TImage, TValue> cardDeckCreator,
				BlackJackCardHandCreator<TImage, TValue> cardHandCreator,
				BlackJackPlayerHandsCreator<TImage, TValue> playerCardHandsCreator,
				BlackJackPlayerCreator<TImage, TValue> playerCreator,
				Subject<BetUpdateEvent> betUpdateEvent,

				IDealerServices<TImage, TValue> dealerServices,
				PlayerServices<TImage, TValue> playerServices,
				GameRuleServices<TImage, TValue> gameRuleServices
			) 
		{
			//TODO:: Seperate into different services, player, dealer, evaluation?!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
			PlayerChangedEvent = new Subject<Dictionary<string, IPlayer<TImage, TValue>>>();
			BetUpdateEvent = betUpdateEvent;
			BetRequestedEvent = new Subject<IPlayer<TImage, TValue>>();

			_betInputTask = new Dictionary<string, TaskCompletionSource<int>>();

			Players = new Dictionary<string, IPlayer<TImage, TValue>>();

			
			_cardHandCreator = cardHandCreator;
			_playerCardHandsCreator = playerCardHandsCreator;
			_playerCreator = playerCreator;
			

			_cardDeck = cardDeckCreator.CreateDeck();
			
			//roundEvaluator = GameLogicCreator<TImage, TValue>.CreateRoundEvaluator();
			_dealerServices = dealerServices;
			_playerServices = playerServices;
			_gameRuleServices = gameRuleServices;
			
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



		// Initiates and wait for player bets retrival
		public async Task RegisterBetForNewRound()
		{
			foreach (KeyValuePair<string, IPlayer<TImage, TValue>> player in Players)
			{

				string playerName = player.Key;
				IPlayer<TImage, TValue> currentPlayer = player.Value;

				// Adds a new completion source for the bet input task
				TaskCompletionSource<int> betInputTask = new TaskCompletionSource<int>();
				_betInputTask[playerName] = betInputTask;

				// Notify which player is to place their bet
				BetRequestedEvent.OnNext(currentPlayer);

				// Change the state to show that logic is waiting for the bet input
				UpdateGameState(state => state.IsBetRecieved = false);

				// Wait for the bet input to be received
				int betInput = await betInputTask.Task; ;

				// Change the state to show that logic is waiting for the bet input
				UpdateGameState(state => state.IsBetRecieved = true);

				Debug.WriteLine($"Bet input received for {playerName}: {betInput}");
			}

			Debug.WriteLine("All bet inputs received. Starting new round.");
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


		// Starts a new round of the game, handles player turns and dealer's turn
		public async Task StartNewRound()
		{
			_cardDeck.ShuffleDeck();

			// Gives dealer his initial cards
			_dealerServices.InitialDeal(_dealerServices.DealerCardHand, _cardDeck);

			foreach (KeyValuePair<string, IPlayer<TImage, TValue>> player in Players)
			{
				// Player conducts their turn
				await _playerServices.PlayerRound.PlayerTurn(_cardDeck, player.Value);
			}

			// Dealer finishes his turn
			_dealerServices.DealerFinishTurn(_dealerServices.DealerCardHand, _cardDeck);

			EvaluateRound();

		}


		public void EvaluateRound()
		{
			// Evaluate the round and determine the winner
			//Debug.WriteLine(roundEvaluator.EvaluateRound(PlayerCardHand.PrimaryCardHand, DealerCardHand.PrimaryCardHand));
		}

		private void EvaluateSingleHand(BlackJackCardHand<TImage, TValue> cardHand)
		{

		}

	

		// This method is called when the player changes
		// Notify subscribers about the player changes
		public void OnPlayerChangedReceived(List<string> playerNames)
		{
			Players.Clear();

			foreach (string playerName in playerNames)
			{
				Players.Add(playerName,
					_playerCreator.CreatePlayer(
						_playerCardHandsCreator.CreatePlayerHands(HandOwners.HandOwner.Player, _cardHandCreator),
						BetUpdateEvent,
						playerName)
					);
			}

			PlayerChangedEvent.OnNext(Players);
		}
	}
}

