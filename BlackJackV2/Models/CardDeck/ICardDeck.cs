using BlackJackV2.Models.CardFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackV2.Models.CardDeck
{

	/** 
	 * A Generic interface for a set of Generic Card objects
	 * 
	 * Minimum functionality: shuffle the deck, get the top card
	 **/

	public interface ICardDeck<TImage,TValue>
	{
		public ICard<TImage, TValue> GetTopCard();
		public void ShuffleDeck();
	}
}
