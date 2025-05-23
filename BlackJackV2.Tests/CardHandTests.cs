using Avalonia.Media.Imaging;
using BlackJackV2.Factories.CardFactory;
using BlackJackV2.Models.Card;
using BlackJackV2.Models.CardDeck;
using BlackJackV2.Models.CardHand;
using BlackJackV2.Shared.Utilities.ImageLoader;
using DynamicData;
using Newtonsoft.Json.Linq;
using System.ComponentModel;

namespace BlackJackV2.Tests
{
	public class CardHandTests
	{
		// card value has format "Suit_Value"
		// Values: 1 - 13  
		FakeCardDeckCreator fakeCardDeckCreator = new FakeCardDeckCreator();


		/// <summary>
		/// This test checks if the CalculateHandValue method correctly calculate a hand with a court card.
		/// </summary>
		[Fact]
		public void CalculateHandValue_NoAce_PipCardAndCourtCard()
		{
			// Arrange
			fakeCardDeckCreator.AddCardToDeck("Hearts_2");
			fakeCardDeckCreator.AddCardToDeck("Spades_12");

			FakeCardDeck cardDeck = new FakeCardDeck(
				fakeCardDeckCreator.GetCardDeck()
			);

			IBlackJackCardHand<Bitmap, string> cardHand = new BlackJackCardHand<Bitmap, string>();

			int expectedHandValue = 12;

			// Act
			cardHand.AddCard(cardDeck.DrawCard());
			cardHand.AddCard(cardDeck.DrawCard());

			// Assert
			Assert.Equal(expectedHandValue, cardHand.HandValue);
		}

		
		
			
	}


	// Creates a mock/fake version of carddeckcreator
	public class FakeCardDeckCreator
	{
		FakeCardCreator cardCreator = new FakeCardCreator();
		List<ICard<Bitmap, string>> cards = new List<ICard<Bitmap, string>>();
		
		public void AddCardToDeck(string value) 
		{
			Bitmap? cardBackImage = null; 
			Bitmap? cardFrontImage = null;
			
			ICard<Bitmap, string> card = cardCreator.CreateCard(cardFrontImage, cardBackImage, value);

			cards.Add(card);
		}

		public void ClearDeck() 
		{ 
			cards.Clear(); 
		}

		public List<ICard<Bitmap, string>> GetCardDeck()
		{
			return cards;
		}
	}



	// Creates a mock/fake version of a card deck
	public class FakeCardDeck
	{
		private List<ICard<Bitmap, string>> _activeDeck;

		public FakeCardDeck(List<ICard<Bitmap, string>> cardDeck)
		{
			_activeDeck = cardDeck;
		}

		
		public ICard<Bitmap, string> DrawCard()
		{
			ICard<Bitmap, string> topCard = _activeDeck[0];
			_activeDeck.RemoveAt(0);

			return topCard;
		}
	}

	// Creates a mock/fake version of the cardCreator
	public class FakeCardCreator : BlackJackCardCreator<Bitmap, string>
	{
		public ICard<Bitmap, string> CreateCard(Bitmap frontImage, Bitmap backImage, string value)
		{
			return new BlackJackCard<Bitmap, string>(frontImage, backImage, value);
		}
	}
}