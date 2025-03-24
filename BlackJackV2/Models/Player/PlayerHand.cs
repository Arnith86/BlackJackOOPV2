using Avalonia.Controls.Shapes;
using Avalonia.Media.Imaging;
using BlackJackV2.Models.CardHand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackV2.Models.Player
{
	/**
	 * Handles a player's card hands in a blackjack game.
	 * - The first element represents the player's primary hand.
	 * - Additional hands are created when the player splits a hand, and the new hand is added to the list.
	 * - When hands are reset, only the primary hand is kept 
	 *
	 * List<> _cardHands: getter for all current hands
	 * SplitHand()		: Splits a hand into two hands. The card chosen for the split is removed and placed in a new hand
	 * ResetHand()		: Removes all hands exept primary hand, which is reset
	 */

	internal class PlayerHand : IPlayerHand<Bitmap, Bitmap, string>
	{
		private List<ICardHand<Bitmap, Bitmap, string>> _cardHands;
		public List<ICardHand<Bitmap, Bitmap, string>> CardHands => _cardHands;

		public PlayerHand(ICardHand<Bitmap, Bitmap, string> cardHand) 
		{
			_cardHands = new List<ICardHand<Bitmap, Bitmap, string>>();  // consider sending in a list instead
			_cardHands.Add(cardHand);
		}


		// Splits a hand into two hands. The card chosen for the split is removed and placed in a new hand
		public void SplitHand(string splitValue, ICardHand<Bitmap, Bitmap, string> splitHand  )
		{
			// Adds the new empty hand to the list, if not null.
			_cardHands.Add(splitHand ?? throw new ArgumentNullException(nameof(splitHand), "Split hand cannot be null."));

			// Finds the hand which contain the chosen card to split
			foreach ( ICardHand<Bitmap, Bitmap, string> currentCardHand in _cardHands)
			{
				// Searches for the chosen card, in current hand
				ICard<Bitmap, Bitmap, string> cardWithMatchingValue = currentCardHand.Hand.Find(card => card.Value == splitValue);

				// Card has been found, move card end search
				if (cardWithMatchingValue != null)
				{
					splitHand.AddCard(cardWithMatchingValue);
					currentCardHand.RemoveCard(cardWithMatchingValue.Value);

					break;
				}
			}
		}

		// Removes all hands exept primary hand, which is reset
		public void ResetHand()
		{
			_cardHands.First().ClearHand();
			if (_cardHands.Count > 1) _cardHands.RemoveRange(1, _cardHands.Count - 1);
		}

	}
}
