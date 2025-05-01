// Project: BlackJackV2
// file: BlackJackV2/Models/GameLogic/PlayerServices/IPlayerServices.cs

using BlackJackV2.Models.Player;
using BlackJackV2.Services.Events;
using System;
using System.Collections.Generic;
using System.Reactive.Subjects;
using System.Threading.Tasks;

namespace BlackJackV2.Models.GameLogic.PlayerServices
{
	/// <summary>
	/// Defines the contract for managing player services including players, actions, rounds, and bets.
	/// </summary>
	/// <typeparam name="TImage">The type used for representing card images.</typeparam>
	/// <typeparam name="TValue">The type used for representing card values.</typeparam>
	public interface IPlayerServices<TImage, TValue>
	{
		/// <summary>
		/// Gets the collection of players currently participating in the game.
		/// </summary>
		public Dictionary<string, IPlayer<TImage, TValue>> Players { get; }

		/// <summary>
		/// Gets the event subject that notifies subscribers when the player collection changes.
		/// </summary>
		public 	Subject<Dictionary<string, IPlayer<TImage, TValue>>> PlayerChangedEvent { get; }

		/// <summary>
		/// Gets the event subject that notifies subscribers when a player's bet has been updated.
		/// </summary>
		public Subject<BetUpdateEvent> BetUpdateEvent { get; }

		///// <summary>
		///// Gets the event subject that requests a player to place their bet.
		///// </summary>
		//public Subject<IPlayer<TImage, TValue>> BetRequestedEvent { get; }

		/// <summary>
		/// Gets the service that manages player actions such as hit, stand, split, and fold.
		/// </summary>
		IPlayerAction<TImage, TValue> PlayerAction { get; }

		/// <summary>
		/// Gets the service that manages the flow and logic of a player's round, including handling multiple hands.
		/// </summary>
		IPlayerRound<TImage, TValue> PlayerRound { get; }

		/// <summary>
		/// Gets an observable that notifies subscribers when the game state changes.
		/// </summary>
		IObservable<GameState> GameStateObservable { get; }

		/// <summary>
		/// Recreates the player list based on a new set of player names and notifies subscribers about the change.
		/// </summary>
		/// <param name="playerNames">The list of new player names.</param>
		void OnPlayerChangedReceived(List<string> playerNames);

		/// <summary>
		/// Initiates the process of registering player bets for a new round by requesting input from each player.
		/// </summary>
		Task RegisterBetForNewRound();

		/// <summary>
		/// Called when a player inputs their bet, completing the registration process for that player.
		/// </summary>
		/// <param name="playerName">The name of the player who placed the bet.</param>
		/// <param name="betInput">The amount of the bet placed by the player.</param>
		void OnBetInputReceived(string playerName, int betInput);
	}
}