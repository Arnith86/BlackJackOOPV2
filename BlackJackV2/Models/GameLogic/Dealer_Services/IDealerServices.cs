// Project: BlackJackV2
// file: BlackJackV2/Models/GameLogic/Dealer_Services/DealerLogic.cs

using Avalonia.Media.Imaging;
using BlackJackV2.Models.CardDeck;
using BlackJackV2.Models.PlayerHands;

namespace BlackJackV2.Models.GameLogic.Dealer_Services
{
	/// <summary>
	/// Interface for dealer-specific services in a blackjack game,
	/// including the initial deal and the dealer's logic during their turn.
	/// </summary>
	public interface IDealerServices<TImage, TValue>
	{
		/// <summary>
		/// Performs the initial deal for the dealer, drawing two cards.
		/// The first card is face down (back of card), and the second is face up.
		/// </summary>
		/// <param name="dealerHands">
		/// The dealer's hand container. Only the <see cref="IBlackJackPlayerHands{TImage, TValue}.PrimaryCardHand"/> is used.
		/// </param>
		/// <param name="cardDeck">The card deck used for drawing cards.</param>
		public void InitialDeal(IBlackJackPlayerHands<TImage, TValue> dealerHands, ICardDeck<TImage, TValue> cardDeck);

		/// <summary>
		/// Plays out the dealer's turn by drawing cards until the hand value reaches at least 17.
		/// </summary>
		/// <param name="dealerHands">
		/// The dealer's hand container. Only the <see cref="IBlackJackPlayerHands{TImage, TValue}.PrimaryCardHand"/> is used.
		/// </param>
		/// <param name="cardDeck">The card deck used for drawing cards.</param>
		public void DealerFinishTurn(IBlackJackPlayerHands<TImage, TValue> dealerHands, ICardDeck<TImage, TValue> cardDeck);
	}
}
