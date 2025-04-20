// Project: BlackJackV2
// file: BlackJackV2/Factories/CardDeckFactory/BlackJackDeckBuilder.cs


/// <summary>
///		Interface for the creator class to create CardDeck objects. Part of the Card Deck factory pattern 
/// </summary>

using BlackJackV2.Models.CardDeck;


namespace BlackJackV2.Factories.CardDeckFactory
{
	public abstract class ICardDeckCreator<TImage, TValue>
	{
		public abstract ICardDeck<TImage, TValue> CreateDeck();
	}
}
