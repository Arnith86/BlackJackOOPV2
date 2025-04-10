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
		// Represents the player's primary hand and split hand
		private IBlackJackCardHand<Bitmap, string> _primeryCardHand;
		private IBlackJackCardHand<Bitmap, string> _splitCardHand;

		public IBlackJackCardHand<Bitmap, string> PrimaryCardHand => _primeryCardHand;
		public IBlackJackCardHand<Bitmap, string> SplitCardHand => _splitCardHand;

		public PlayerHands(IBlackJackCardHand<Bitmap, string> cardHand) 
		{
			_primeryCardHand = cardHand;
			_splitCardHand = BlackJackCardHandCreator.CreateBlackJackCardHand();
		}


		// Splits a hand into two hands. The card chosen for the split is removed and placed in a new hand
		public bool SplitHand()
		{
			// Checks if the primary hand has two cards
			if (_primeryCardHand.Hand.Count == 2 && _splitCardHand.Hand.Count < 1)
			{
				// Retrieves the value of the first two cards in the primary hand
				string value1 = _primeryCardHand.Hand[0].Value.Split('_')[1];
				string value2 = _primeryCardHand.Hand[1].Value.Split('_')[1];

				// Checks to see if the numeric value of the cards are the same 
				if (value1 == value2)
				{
					// Splits the hand
					_splitCardHand.AddCard(_primeryCardHand.Hand[1]);
					_primeryCardHand.RemoveCard(_primeryCardHand.Hand[1].Value);
					return true;
				}
			}
			
			return false;
		}

		// Empties all hands of cards
		public void ResetHand()
		{
			_primeryCardHand.ClearHand();
			_splitCardHand.ClearHand();
		}
	}
}
