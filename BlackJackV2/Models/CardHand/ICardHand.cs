using BlackJackV2.Models.CardFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackV2.Models.CardHand
{
	/**
	 * Card Hand interface 
	 * Basic funtionallity present
	 * 
	 * HandValue:	Get the current integer value of hand. 
	 * Hand:		Get the list of card objects of hand
	 * AddCard():	Adds a new card object to the hand 
	 * ClearHand()  Emptys the hand
	 * 
	 **/

	internal interface ICardHand<TFrontImage, TBackImage, TValue>
	{
		public List<ICard<TFrontImage, TBackImage, TValue>> Hand { get; }
		public int HandValue { get; }
		public void AddCard(ICard<TFrontImage, TBackImage, TValue> card);
		public void ClearHand();
	}
}
