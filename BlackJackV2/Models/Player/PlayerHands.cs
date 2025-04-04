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
	 *	Handles a player's card hands in a blackjack game.
	 *	When hands are reset, only the primary hand is kept 
	 *
	 *	_PrimeryCardHand	: The player's primary hand
	 *	_splitCardHand		: The player's split hand
	 *	SplitHand()			: Splits a hand into two hands. The card chosen for the split is removed and placed in a new hand
	 *	ResetHand()			: Empteys all hands of cards
	 */

	public class PlayerHands : IPlayerHands<Bitmap, string>
	{
		private ICardHand<Bitmap, string> _primeryCardHand;
		private ICardHand<Bitmap, string> _splitCardHand;

		public ICardHand<Bitmap, string> PrimaryCardHand => _primeryCardHand;
		public ICardHand<Bitmap, string> SplitCardHand => _splitCardHand;

		public PlayerHands(ICardHand<Bitmap, string> cardHand) 
		{
			_primeryCardHand = cardHand;
			_splitCardHand = BlackJackCardHandCreator.CreateBlackJackCardHand();
		}


		// Splits a hand into two hands. The card chosen for the split is removed and placed in a new hand
		public void SplitHand(string splitValue)
		{
			// Searches for the chosen card, in the primary hand
			ICard<Bitmap, string> cardWithMatchingValue = _primeryCardHand.Hand.FirstOrDefault(card => card.Value == splitValue);

			// Card has been found, move card to split hand and end search
			if (cardWithMatchingValue != null)
			{
				_splitCardHand.AddCard(cardWithMatchingValue);
				_primeryCardHand.RemoveCard(cardWithMatchingValue.Value);
			}
		}

		// Removes all hands exept primary hand, which is reset
		public void ResetHand()
		{
			_primeryCardHand.ClearHand();
			_splitCardHand.ClearHand();
		}
	}
}
