using Avalonia.Media.Imaging;
using BlackJackV2.Models.CardFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackV2.Models.CardDeck
{
	/**
	 * This class sole responsability is to create the black jack card deck
	 * It creates a list of card objects with help from the deckBuilder. 
	 **/

	internal class BlackJackCardDeckCreator
	{
		public ICardDeck<Bitmap, Bitmap, string> CreateBlackJackCardDeck()
		{
			BlackJackCardCreator blackJackCardCreator = new BlackJackCardCreator();
			BlackJackDeckBuilder deckBuilder = new BlackJackDeckBuilder();

			return new BlackJackCardDeck(deckBuilder.CreateDeck(blackJackCardCreator));
		}
	}
}
