using Avalonia.Media.Imaging;
using BlackJackV2.Models.CardFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackV2.Models.CardDeck
{
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
