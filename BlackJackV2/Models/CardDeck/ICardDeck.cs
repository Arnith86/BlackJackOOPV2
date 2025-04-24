// Project: BlackJackV2
// file: BlackJackV2/Models/CardDeck/ICardDeck.cs

using BlackJackV2.Models.Card;

namespace BlackJackV2.Models.CardDeck
{
	/// <summary>
	/// Represents a generic set of card objects comprising a card deck.
	/// This interface serves as the product in the CardDeck factory pattern, using generics for card image and value types to support reuse across games.
	/// </summary>
	/// <typeparam name="TImage">The type representing the image used on the card (e.g., bitmap).</typeparam>
	/// <typeparam name="TValue">The type representing the card's value (e.g., string, int).</typeparam>
	/// <remarks> Related files <see cref="BlackJackV2.Factories.CardDeckFactory"/></remarks>
	public interface ICardDeck<TImage,TValue>
	{
		/// <summary>
		/// Removes and returns the top card from the deck.
		/// </summary>
		/// <returns>The Top <see cref="ICard{TImage, TValue}"/> from the deck./></returns>
		public ICard<TImage, TValue> GetTopCard();
		
		/// <summary>
		/// Shuffles the deck to randomize the order of the cards.
		/// </summary>
		public void ShuffleDeck();
	}
}
