// Project: BlackJackV2
// file: BlackJackV2/Models/CardDeck/BlackJackCardDeck.cs

using System;
using System.Collections.Generic;
using System.Linq;
using BlackJackV2.Models.Card;

namespace BlackJackV2.Models.CardDeck
{
	/// <summary>
	/// Represents a standard BlackJack card deck that suppors shuffeling and retrieval of cards.
	/// Implements <see cref="ICard{TImage, TValue}"/> to define game specific behavior.  
	/// </summary>
	/// <remarks> Related files <see cref="BlackJackV2.Factories.CardDeckFactory"/></remarks>
	public class BlackJackCardDeck<TImage, TValue> : ICardDeck<TImage, TValue>
	{
		/// <summary>
		/// Holds the original unshuffled deck, used to regenerate the active deck.
		/// </summary>
		private readonly List<ICard<TImage, TValue>> _originalDeck;

		/// <summary>
		/// Holds the current shuffled card deck in play.
		/// </summary>
		private List<ICard<TImage, TValue>> _activeDeck;

		/// <summary>
		/// Initializes a new instance of the <see cref="BlackJackCardDeck"/> class, with a specific set of cards. 
		/// </summary>
		/// <param name="cardDeck">The full set of cards used for shuffeling and drawing.</param>
		public BlackJackCardDeck(List<ICard<TImage, TValue>> cardDeck)
		{
			_originalDeck = cardDeck;
			_activeDeck = new List<ICard<TImage, TValue>>();
		}

		/// <inheritdoc/>
		public ICard<TImage, TValue> GetTopCard()
		{
			ICard<TImage, TValue> topCard = _activeDeck[0];
			_activeDeck.RemoveAt(0);

			return topCard;
		}

		/// <summary>
		/// Shuffles the deck and resets the active deck.
		/// </summary>
		public void ShuffleDeck()
		{
			Random random = new Random();
			_activeDeck.Clear();
			_activeDeck = _originalDeck.OrderBy(x => random.Next()).ToList();
		}
	}
}
