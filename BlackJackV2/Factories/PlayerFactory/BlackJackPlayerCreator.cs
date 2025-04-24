// Project: BlackJackV2
// file: BlackJackV2/Factories/PlayerFactory/BlackJackPlayerCreator.cs

using Avalonia.Media.Imaging;
using BlackJackV2.Models.Player;
using BlackJackV2.Models.PlayerHands;
using BlackJackV2.Services.Events;
using System.Reactive.Subjects;

namespace BlackJackV2.Factories.PlayerFactory
{
	/// <summary>
	/// Concrete creator class for the Player factory pattern, responsible for creating fully initialized <see cref="Player"/> objects.
	/// Used to instantiate players with names, hands, and reactive bet handling in a Blackjack game.
	/// </summary>
	/// <remarks> Related files <see cref="BlackJackV2.Models.Player"/></remarks>
	public class BlackJackPlayerCreator : PlayerCreator<Bitmap, string>
	{
		/// <summary>
		/// Creates a new Blackjack player, initializing it with the provided hands, name, and bet update notification mechanism.
		/// </summary>
		/// <param name="playerHands">
		/// Represents the collection of hands the player can hold in the game, which may include a primary hand and one or more split hands.
		/// </param>
		/// <param name="betUpdatedSubject">
		/// A mechanism (subject) for notifying when a bet update occurs for a specific hand, enabling reactive updates.
		/// </param>
		/// <param name="name">
		/// The name of the player. Defaults to "Player1" if not specified.
		/// </param>
		/// <returns>
		/// A new instance of <see cref="Player"/> representing the created Blackjack player.
		/// </returns>
		public override IPlayer CreatePlayer(IBlackJackPlayerHands<Bitmap, string> playerHands, ISubject<BetUpdateEvent> betUpdatedSubject, string name = "Player1")
		{
			return new Player(name, playerHands, betUpdatedSubject);
		}
	}
}
