// Project: BlackJackV2
// file: BlackJackV2/Models/GameLogic/PlayerServices/PlayerServices.cs

using BlackJackV2.Factories.PlayerFactory;
using BlackJackV2.Models.GameLogic.CardServices;
using BlackJackV2.Models.Player;
using BlackJackV2.Services.Events;
using BlackJackV2.Shared.Constants;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
		private readonly Subject<SplitEvent> _splitEvent;
		private readonly Subject<BetRequestEvent<TImage, TValue>> _betRequestedEvent;

		// Used to wait for specific player bet input to be received
		private ConcurrentDictionary<string, TaskCompletionSource<int>> _betInputTask;
			

		public PlayerServices(	
			Dictionary<string, IPlayer<TImage, TValue>> players,
			ICardServices<TImage, TValue> cardServices,
			IPlayerAction<TImage, TValue> playerAction, 
			IPlayerRound<TImage, TValue> playerRound,
			BlackJackPlayerCreator<TImage, TValue> playerCreator,
			Subject<BetUpdateEvent> betUpdateEvent,
			Subject<BetRequestEvent<TImage, TValue>> betRequestEvent,
			Subject<SplitEvent> splitEvent)
		{
			_players = players;
			_cardServices = cardServices;
			_playerAction = playerAction;
			_playerRound = playerRound;
			_playerCreator = playerCreator;
			_playerChangedEvent = new Subject<Dictionary<string, IPlayer<TImage, TValue>>>();
			_betUpdateEvent = betUpdateEvent;
			_betRequestedEvent = betRequestEvent;
			_splitEvent = splitEvent;
			_betInputTask = new ConcurrentDictionary<string, TaskCompletionSource<int>>();
		}


		/// <inheritdoc/>
		public Dictionary<string, IPlayer<TImage, TValue>> Players => _players;

		/// <inheritdoc/>
		public Subject<Dictionary<string, IPlayer<TImage, TValue>>> PlayerChangedEvent => _playerChangedEvent;
		
		/// <inheritdoc/>
		public Subject<BetUpdateEvent> BetUpdateEvent => _betUpdateEvent;
		
		/// <inheritdoc/>
		public IPlayerAction<TImage, TValue> PlayerAction => _playerAction;
		
		/// <inheritdoc/>
		public IPlayerRound<TImage, TValue> PlayerRound => _playerRound;

		
		/// <inheritdoc/>
		public void OnPlayerChangedReceived(ObservableCollection<PlayerNameEntry> playerNames)
		{
			Players.Clear();

			foreach (PlayerNameEntry playerName in playerNames)
			{
				Players.Add(playerName.PlayerName,
					_playerCreator.CreatePlayer(
						_cardServices.GetNewPlayerHands(HandOwners.HandOwner.Player),
						BetUpdateEvent,
						playerName.PlayerName)
					);
			}

			PlayerChangedEvent.OnNext(Players);
		}

		///<inheritdoc/>
		public async Task RegisterBetForNewRound()
		{
			// All bet tasks are stored in this list, after the loop is done,
			// we will await for all of them to complete.
			var betTasks = new List<Task>();

			foreach (var (playerName, currentPlayer) in Players)
			{
				// Adds a new completion source for the bet input task
				var betInputTask = new TaskCompletionSource<int>();
				_betInputTask[playerName] = betInputTask;

				// Notify which player is to place their bet
				_betRequestedEvent.OnNext(new BetRequestEvent<TImage, TValue>(currentPlayer, false));

				betTasks.Add(betInputTask.Task);
			}

			// Wait for all bet tasks to complete
			await Task.WhenAll(betTasks);

			Debug.WriteLine("All bet inputs received. Starting new round.");
		}

		/// <inheritdoc/>
		public void OnBetInputReceived(string playerName, int betInput)
		{
			if (_betInputTask.TryRemove(playerName, out var betUpdateCompletionSource))
			{
				Players[playerName].PlaceBet(HandOwners.HandOwner.Primary, betInput);
				betUpdateCompletionSource.SetResult(betInput);
				Debug.WriteLine($"Bet input received for {playerName}: {betInput}");
			}
		}

		/// <inheritdoc/>
		public void ResetPlayerCardHands()
		{
			foreach (var (playerName, currentPlayer) in _players)
			{
				_splitEvent.OnNext(new SplitEvent(playerName, false));
				currentPlayer.Hands.ResetHand();
				_betUpdateEvent.OnNext(new BetUpdateEvent(playerName, HandOwners.HandOwner.Primary));
			}
		}
	}
}
