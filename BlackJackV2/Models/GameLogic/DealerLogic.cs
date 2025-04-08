using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using BlackJackV2.Models.CardDeck;
using BlackJackV2.Models.Player;
using BlackJackV2.Models.CardFactory;
using HarfBuzzSharp;

namespace BlackJackV2.Models.GameLogic
{
	public class DealerLogic
	{

		/**
		 *	Handles the dealer's turn in a blackjack game.
		 *	Contains the logic for the dealer's turn, including the initial deal and the dealer's actions during their turn.
		 * 
		 *	InitialDeal()		: Deals two cards to the dealer, the first card is flipped face down, the second card is face up
		 *  DealerFinishTurn()	: Dealer finishes their turn by hitting until they reach a total of 17 or higher (or busts)
		 *  
		 */

		// Deals two cards to the dealer, the first card is flipped face down, the second card is face up
		public void InitialDeal(IPlayerHands<Bitmap, string> dealerHands, ICardDeck<Bitmap, string> cardDeck)
		{
			BlackJackCard firstCard = (BlackJackCard)cardDeck.GetTopCard();
			firstCard.FlipCard();

			dealerHands.PrimaryCardHand.AddCard(firstCard);
			dealerHands.PrimaryCardHand.AddCard((BlackJackCard)cardDeck.GetTopCard());
		}

		// Dealer finishes their turn by hitting until they reach a total of 17 or higher (or busts)
		public void DealerFinishTurn(IPlayerHands<Bitmap, string> dealerHands, ICardDeck<Bitmap, string> cardDeck)
		{
			// Flip the first card face up
			dealerHands.PrimaryCardHand.Hand[0].FlipCard();

			// Dealer must hit until they reach a total of 17 or higher
			while (dealerHands.PrimaryCardHand.HandValue < 17)
			{ 
				dealerHands.PrimaryCardHand.AddCard((BlackJackCard)cardDeck.GetTopCard());
			}
		}
	}
}
