using Avalonia.Controls.Shapes;
using Avalonia.Media.Imaging;
using BlackJackV2.Constants;
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
		// The id of the hand, used to identify the hand in the game
		public HandOwners.HandOwner Id { get; private set; } 

		// Represents the player's primary hand and split hand
		private BlackJackCardHand _primeryCardHand;
		private BlackJackCardHand _splitCardHand;

		public BlackJackCardHand PrimaryCardHand => _primeryCardHand;
		public BlackJackCardHand SplitCardHand => _splitCardHand;

		public PlayerHands(HandOwners.HandOwner id, IBlackJackCardHand<Bitmap, string> cardHand) 
		{
			Id = id;
			_primeryCardHand = (BlackJackCardHand)cardHand;
			_splitCardHand = BlackJackCardHandCreator.CreateBlackJackCardHand();

			if (Id == HandOwners.HandOwner.Player)
			{
				_primeryCardHand.Id = HandOwners.HandOwner.Primary;
				_splitCardHand.Id = HandOwners.HandOwner.Split;
			}
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
