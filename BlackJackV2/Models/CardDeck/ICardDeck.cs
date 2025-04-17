// Project: BlackJackV2
// file: BlackJackV2/Models/CardDeck/ICardDeck.cs

/// <summary>
/// 
///		A Generic interface for a set of Generic Card objects
///		Minimum functionality: shuffle the deck, get the top card
///		
///		ICard<TImage, TValue>	GetTopCard()	: Returns the top card of a card deck.
///		void					ShuffleDeck()	: Shuffles the card deck.
///		
/// </summary>

namespace BlackJackV2.Models.CardDeck
{
	public interface ICardDeck<TImage,TValue>
	{
		public ICard<TImage, TValue> GetTopCard();
		public void ShuffleDeck();
	}
}
