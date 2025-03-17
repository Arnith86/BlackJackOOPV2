using BlackJackV2.Models.CardFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackV2.Models
{

	/* * 
	 * A Generic interface for a set of Generic Card objects
	 * Minimum functionality: shuffle the deck, get the top card
	 * */

	internal interface ICardDeck <TFrontImage, TBackImage, TValue>
	{
		public ICard<TFrontImage, TBackImage, TValue> GetTopCard();
		public void shuffleDeck();
	}
}
