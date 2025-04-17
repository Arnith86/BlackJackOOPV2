// Project: BlackJackV2
// file: BlackJackV2/Models/CardDeck/BlackJackCardDeckCreator.cs

/// <summary>
///		
///		This class sole responsability is to create the black jack card deck
///		It creates a list of card objects with help from the deckBuilder. 
///		
///		ICardDeck<Bitmap, string> CreateBlackJackCardDeck(): Returns a BlackJackCardDeck object
///		
/// </summary>

using Avalonia.Media.Imaging;
using BlackJackV2.Models.CardFactory;


namespace BlackJackV2.Models.CardDeck
{
	internal class BlackJackCardDeckCreator
	{
		public static ICardDeck<Bitmap, string> CreateBlackJackCardDeck()
		{
			BlackJackCardCreator blackJackCardCreator = new BlackJackCardCreator();
			BlackJackDeckBuilder deckBuilder = new BlackJackDeckBuilder();

			return new BlackJackCardDeck(deckBuilder.CreateDeck(blackJackCardCreator));
		}
	}
}
