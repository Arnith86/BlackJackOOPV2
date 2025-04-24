// Project: BlackJackV2
// file: BlackJackV2/Models/GameLogic/PlayerServices/IPlayerAction.cs

using Avalonia.Media.Imaging;
using BlackJackV2.Models.CardDeck;
using BlackJackV2.Models.CardHand;
using BlackJackV2.Models.Player;
using BlackJackV2.Models.PlayerHands;

namespace BlackJackV2.Models.GameLogic.PlayerServices
{
	/// <summary>
	/// Interface handling possible player actions for a Blackjack player's hand.
	/// </summary>
	public interface IPlayerAction
	{
		/// <summary>
		/// Performs the Hit action on a specific hand.
		/// </summary>
		/// <param name="playerHands">The container for the players hands.</param>
		/// <param name="cardHand">The specific hand which the hit action is to be performed on.</param>
		/// <param name="blackJackCardDeck">The deck which the card object is drawn from.</param>
		public void PerformHit(IBlackJackPlayerHands<Bitmap, string> playerHands,
								IBlackJackCardHand<Bitmap, string> cardHand,
								ICardDeck<Bitmap, string> blackJackCardDeck);

		/// <summary>
		/// Performes the Double Down action on a specific hand.
		/// </summary>
		/// <param name="playerFunds">The players current funds.</param>
		/// <param name="playerHands">The container for the players hands.</param>
		/// <param name="cardHand">The specific hand which the double down action is to be performed on.</param>
		/// <param name="blackJackCardDeck">The deck which the card object is drawn from.</param>
		public void PerformDoubleDown(	IPlayer player,
										IBlackJackCardHand<Bitmap, string> cardHand,
										ICardDeck<Bitmap, string> blackJackCardDeck);

		/// <summary>
		/// Performes the Fold action on a specific hand. 
		/// </summary>
		/// <param name="playerHands">The container for the players hands.</param>
		/// <param name="cardHand">The specific hand which the fold action is to be performed on.</param>
		/// <param name="blackJackCardDeck">The deck which the card object is drawn from.</param>
		public void PerformFold(	IBlackJackPlayerHands<Bitmap, string> playerHands,
									IBlackJackCardHand<Bitmap, string> cardHand,
									ICardDeck<Bitmap, string> blackJackCardDeck);
		/// <summary>
		/// Performes the Split action.
		/// </summary>
		/// <param name="playerName">The name of the player performing the action.</param>
		/// <param name="playerFunds">The players current funds.</param>
		/// <param name="playerHands">The container for the players hands.</param>
		/// <param name="blackJackCardDeck">The deck which the card object is drawn from.</param>
		public void PerformSplit(IPlayer player, ICardDeck<Bitmap, string> blackJackCardDeck);

	}
}
