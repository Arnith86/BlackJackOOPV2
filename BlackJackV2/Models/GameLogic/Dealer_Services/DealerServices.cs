// Project: BlackJackV2
// file: BlackJackV2/Models/GameLogic/Dealer_Services/DealerLogic.cs


using Avalonia.Media.Imaging;
using BlackJackV2.Models.CardDeck;
using BlackJackV2.Models.Card;
using BlackJackV2.Models.PlayerHands;
using System;

namespace BlackJackV2.Models.GameLogic.Dealer_Services
{
	/// <summary>
	/// This class handles dealer specific services in a blackjack game. 
	/// Including the initial deal and the dealer's logic during their turn.
	/// The dealer only uses the <see cref="IBlackJackPlayerHands{TImage, TValue}.PrimaryCardHand"/>.
	/// </summary>
	public class DealerServices : IDealerServices<Bitmap, string>
	{
		///<inheritdoc/>
		public void InitialDeal(IBlackJackPlayerHands<Bitmap, string> dealerHands, ICardDeck<Bitmap, string> cardDeck)
		{
			ICard<Bitmap, string> firstCard = cardDeck.GetTopCard();
			firstCard.FlipCard();
			dealerHands.PrimaryCardHand.AddCard(firstCard);
			dealerHands.PrimaryCardHand.AddCard(cardDeck.GetTopCard());
		}

		///<inheritdoc/>
		public void DealerFinishTurn(IBlackJackPlayerHands<Bitmap, string> dealerHands, ICardDeck<Bitmap, string> cardDeck)
		{
			dealerHands.PrimaryCardHand.Hand[0].FlipCard();
			dealerHands.PrimaryCardHand.RecalculateHandAfterCardFlip();

			if (dealerHands.PrimaryCardHand.HandValue <= 17)
				DrawUntillSeventeen(dealerHands, cardDeck);
		}

		// Dealer must hit until they reach a total of 17 or higher
		private void DrawUntillSeventeen(IBlackJackPlayerHands<Bitmap, string> dealerHands, ICardDeck<Bitmap, string> cardDeck)
		{
			while (dealerHands.PrimaryCardHand.HandValue < 17)
				dealerHands.PrimaryCardHand.AddCard((BlackJackCard)cardDeck.GetTopCard());
		}
	}
}
