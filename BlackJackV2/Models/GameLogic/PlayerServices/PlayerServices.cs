// Project: BlackJackV2
// file: BlackJackV2/Models/GameLogic/PlayerServices/PlayerServices.cs

using BlackJackV2.Factories.PlayerFactory;
using BlackJackV2.Models.GameLogic.CardServices;
using BlackJackV2.Models.Player;
using BlackJackV2.Services.Events;
using BlackJackV2.Shared.Constants;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;

namespace BlackJackV2.Models.GameLogic.PlayerServices
{
	/// <summary>
	/// Represents a container for player-related services in BlackJack, 
	/// bundling player actions and round management into a single unit.
	/// </summary>
	/// <typeparam name="TImage">The type used for representing card images.</typeparam>
	/// <typeparam name="TValue">The type used for representing card values.</typeparam>
	/// <param name="playerAction">
	/// Provides the logic for handling player actions such as hit, stand, split, and fold.
	/// </param>
	/// <param name="playerRound">
	/// Manages the flow of a player's round, including handling multiple hands.
	/// </param>
	public class PlayerServices<TImage, TValue> : IPlayerServices<TImage, TValue>
	{
		private readonly Dictionary<string, IPlayer<TImage, TValue>> _players;
		private readonly ICardServices<TImage, TValue> _cardServices;
		private readonly IPlayerAction<TImage, TValue> _playerAction;
		private readonly IPlayerRound<TImage, TValue> _playerRound;
		private readonly BlackJackPlayerCreator<TImage, TValue> _playerCreator;

		// Event subjects to notify subscribers about player changes and bet updates
		private readonly Subject<Dictionary<string, IPlayer<TImage, TValue>>> _playerChangedEvent;
		private readonly Subject<BetUpdateEvent> _betUpdateEvent;
		private readonly Subject<IPlayer<TImage, TValue>> _betRequestedEvent;

		// Used to wait for specific player bet input to be received
		private Dictionary<string, TaskCompletionSource<int>> _betInputTask;
		// Subject and IObservable to notify when the game state changes
		private BehaviorSubject<GameState> _gameStateSubject;
		

		public PlayerServices(	Dictionary<string, IPlayer<TImage, TValue>> players,
								ICardServices<TImage, TValue> cardServices,
								IPlayerAction<TImage, TValue> playerAction, 
								IPlayerRound<TImage, TValue> playerRound,
								BlackJackPlayerCreator<TImage, TValue> playerCreator,
								Subject<BetUpdateEvent> betUpdateEvent)
		{
			_players = players;
			_cardServices = cardServices;
			_playerAction = playerAction;
			_playerRound = playerRound;
			_playerCreator = playerCreator;
			_playerChangedEvent = new Subject<Dictionary<string, IPlayer<TImage, TValue>>>();
			_betUpdateEvent = betUpdateEvent;
			_betRequestedEvent = new Subject<IPlayer<TImage, TValue>>();
			_betInputTask = new Dictionary<string, TaskCompletionSource<int>>();
			_gameStateSubject = new BehaviorSubject<GameState>(new GameState());
		}


		/// <inheritdoc/>
		public Dictionary<string, IPlayer<TImage, TValue>> Players => _players;

		/// <inheritdoc/>
		public Subject<Dictionary<string, IPlayer<TImage, TValue>>> PlayerChangedEvent => _playerChangedEvent;
		
		/// <inheritdoc/>
		public Subject<BetUpdateEvent> BetUpdateEvent => _betUpdateEvent;
		
		/// <inheritdoc/>
		public Subject<IPlayer<TImage, TValue>> BetRequestedEvent => _betRequestedEvent;

		/// <inheritdoc/>
		public IPlayerAction<TImage, TValue> PlayerAction => _playerAction;
		
		/// <inheritdoc/>
		public IPlayerRound<TImage, TValue> PlayerRound => _playerRound;

		/// <inheritdoc/>
		public IObservable<GameState> GameStateObservable => _gameStateSubject.AsObservable();

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

		/// <inheritdoc/>
		public void OnPlayerChangedReceived(List<string> playerNames)
		{
			Players.Clear();

			foreach (string playerName in playerNames)
			{
				Players.Add(playerName,
					_playerCreator.CreatePlayer(
						_cardServices.GetNewPlayerHands(HandOwners.HandOwner.Player),
						BetUpdateEvent,
						playerName)
					);
			}

			PlayerChangedEvent.OnNext(Players);
		}

		///<inheritdoc/>
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

		/// <inheritdoc/>
		public void OnBetInputReceived(string playerName, int betInput)
		{
			if (_betInputTask.TryGetValue(playerName, out var betUpdateCompletionSource))
			{
				Players[playerName].PlaceBet(HandOwners.HandOwner.Primary, betInput);
				betUpdateCompletionSource.SetResult(betInput);
				_betInputTask.Remove(playerName);
			}
		}
	}
}
