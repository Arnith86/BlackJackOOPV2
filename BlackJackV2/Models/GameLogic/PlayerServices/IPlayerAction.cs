// Project: BlackJackV2
// file: BlackJackV2/Models/GameLogic/PlayerServices/IPlayerAction.cs

using BlackJackV2.Models.CardDeck;
using BlackJackV2.Models.CardHand;
using BlackJackV2.Models.Player;
using BlackJackV2.Models.PlayerHands;

namespace BlackJackV2.Models.GameLogic.PlayerServices
{
	/// <summary>
	/// Interface handling possible player actions for a Blackjack player's hand.
	/// </summary>
	public interface IPlayerAction<TImage, TValue>
	{
		/// <summary>
		/// Performs the Hit action on a specific hand.
		/// </summary>
		/// <param name="playerHands">The container for the players hands.</param>
		/// <param name="cardHand">The specific hand which the hit action is to be performed on.</param>
		/// <param name="blackJackCardDeck">The deck which the card object is drawn from.</param>
		public void PerformHit(	IBlackJackPlayerHands<TImage, TValue> playerHands,
								IBlackJackCardHand<TImage, TValue> cardHand,
								ICardDeck<TImage, TValue> blackJackCardDeck);

		/// <summary>
		/// Performes the Double Down action on a specific hand.
		/// </summary>
		/// <param name="playerFunds">The players current funds.</param>
		/// <param name="playerHands">The container for the players hands.</param>
		/// <param name="cardHand">The specific hand which the double down action is to be performed on.</param>
		/// <param name="blackJackCardDeck">The deck which the card object is drawn from.</param>
		public void PerformDoubleDown(	IPlayer<TImage, TValue> player,
										IBlackJackCardHand<TImage, TValue> cardHand,
										ICardDeck<TImage, TValue> blackJackCardDeck);

		/// <summary>
		/// Performes the Fold action on a specific hand. 
		/// </summary>
		/// <param name="playerHands">The container for the players hands.</param>
		/// <param name="cardHand">The specific hand which the fold action is to be performed on.</param>
		/// <param name="blackJackCardDeck">The deck which the card object is drawn from.</param>
		public void PerformFold(	IBlackJackPlayerHands<TImage, TValue> playerHands,
									IBlackJackCardHand<TImage, TValue> cardHand,
									ICardDeck<TImage, TValue> blackJackCardDeck);
		/// <summary>
		/// Performes the Split action.
		/// </summary>
		/// <param name="playerName">The name of the player performing the action.</param>
		/// <param name="playerFunds">The players current funds.</param>
		/// <param name="playerHands">The container for the players hands.</param>
		/// <param name="blackJackCardDeck">The deck which the card object is drawn from.</param>
		public void PerformSplit(IPlayer<TImage, TValue> player, ICardDeck<TImage, TValue> blackJackCardDeck);
	}
}
