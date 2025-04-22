// Project BlackJackV2
// file: BlackJackV2/Factories/PlayerFactory/IPlayerCreator.cs

using BlackJackV2.Models.Player;
using BlackJackV2.Models.PlayerHands;
using BlackJackV2.Services.Events;
using System.Reactive.Subjects;

/// <summary>
///     Defines the abstract base class for a creator responsible for creating <see cref="IPlayer"/> objects. 
///     This is part of the Player factory pattern, which instantiates a player in a Blackjack game with specific 
///     hands, bet update handling, and a player name.
/// </summary>



namespace BlackJackV2.Factories.PlayerFactory
{
	public abstract class PlayerCreator<TImage, TValue>
	{
		/// <summary>
		///     Creates a new Blackjack player, initializing the player with the provided hands, name, and bet update mechanism.
		/// </summary>
		/// <param name="playerHands">
		///     Represents the collection of hands the player can hold in the game, which may include a primary hand and one or more split hands.
		/// </param>
		/// <param name="betUpdatedSubject">
		///     A mechanism (subject) for notifying when a bet update occurs for a specific hand, enabling reactive updates.
		/// </param>
		/// <param name="name">
		///     The name of the player.
		/// </param>
		/// <returns>
		///     A new instance of <see cref="IPlayer"/> that represents the created player.
		/// </returns>
		public abstract IPlayer CreatePlayer(IBlackJackPlayerHands<TImage, TValue> playerHands, ISubject<BetUpdateEvent> betUpdatedSubject, string name);
	}
}
