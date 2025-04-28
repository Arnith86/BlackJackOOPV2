// Project BlackJackV2
// file: BlackJackV2/Factories/PlayerHandsFactory/IBlackJackPlayerHandsCreator.cs

using BlackJackV2.Factories.CardHandFactory;
using BlackJackV2.Models.PlayerHands;
using BlackJackV2.Shared.Constants;

namespace BlackJackV2.Factories.PlayerHandsFactory
{
	/// <summary>
	/// Defines the abstract base class for a creator responsible for creating <see cref="IBlackJackPlayerHands{TImage, TValue}"/> objects.
	/// This is part of the Player Hands factory pattern, which instantiates player or dealer hands used in a Blackjack game.
	/// </summary>
	/// <remarks> Related files <see cref="BlackJackV2.Models.Player"/></remarks>
	public abstract class PlayerHandsCreator<TImage, TValue>
	{
		/// <summary>
		/// Creates a new player hands object associated with a specific game participant (player or dealer),
		/// using the provided <see cref="CardHandCreator{TImage, TValue}"/> to generate individual hands.
		/// </summary>
		/// <param name="id">
		/// Identifies the owner of the hands being created (e.g., player or dealer) using the <see cref="HandOwners.HandOwner"/> enum.
		/// </param>
		/// <param name="cardHandCreator">
		/// Factory responsible for generating the underlying card hand(s) that will populate the player hands structure.
		/// </param>
		/// <returns>
		/// An instance of <see cref="IBlackJackPlayerHands{TImage, TValue}"/> representing the initialized player hands.
		/// </returns>
		public abstract IBlackJackPlayerHands<TImage, TValue> CreatePlayerHands(HandOwners.HandOwner id, BlackJackCardHandCreator<TImage, TValue> cardHandCreator);
	}
}
