// Project: BlackJackV2
// file: BlackJackV2/Models/GameLogic/CardServices/ICardServices.cs

using BlackJackV2.Models.CardDeck;
using BlackJackV2.Models.CardHand;
using BlackJackV2.Models.PlayerHands;
using BlackJackV2.Shared.Constants;

namespace BlackJackV2.Models.GameLogic.CardServices
{
	public interface ICardServices<TImage, TValue>
	{
		/// <summary>
		/// Gets the current Blackjack card deck instance.
		/// </summary>
		public ICardDeck<TImage, TValue> CardDeck { get; }

		/// <summary>
		/// Creates and returns a new Blackjack card hand for the specified hand owner.
		/// </summary>
		/// <param name="whichHand">Identifier for which hand is to be created (e.g., Primary or Split).</param>
		/// <returns>A newly created Blackjack card hand.</returns>
		public IBlackJackCardHand<TImage, TValue> GetACardHand(HandOwners.HandOwner whichHand);

		/// <summary>
		/// Creates and returns a new set of player hands associated with the given player identifier.
		/// </summary>
		/// <param name="playerID">The identifier representing the player for whom the hands are created.</param>
		/// <returns>A newly created collection of player hands.</returns>
		public IBlackJackPlayerHands<TImage, TValue> GetNewPlayerHands(HandOwners.HandOwner playerID);
	}
}
