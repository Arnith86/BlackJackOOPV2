// Project: BlackJackV2
// file: BlackJackV2/Models/CardDeck/BlackJackCardDeck.cs

using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Media.Imaging;
using BlackJackV2.Models.Card;

namespace BlackJackV2.Models.CardDeck
{
	/// <summary>
	/// Represents a standard BlackJack card deck that suppors shuffeling and retrieval of cards.
	/// Implements <see cref="ICard{TImage, TValue}"/> to define game specific behavior.  
	/// </summary>
	public class BlackJackCardDeck : ICardDeck<Bitmap, string>
	{
		/// <summary>
		/// Holds the original unshuffled deck, used to regenerate the active deck.
		/// </summary>
		private readonly List<ICard<Bitmap, string>> _originalDeck;

		/// <summary>
		/// Holds the current shuffled card deck in play.
		/// </summary>
		private List<ICard<Bitmap, string>> _activeDeck;

		/// <summary>
		/// Initializes a new instance of the <see cref="BlackJackCardDeck"/> class, with a specific set of cards. 
		/// </summary>
		/// <param name="cardDeck">The full set of cards used for shuffeling and drawing.</param>
		public BlackJackCardDeck(List<ICard<Bitmap, string>> cardDeck)
		{
			_originalDeck = cardDeck;
			_activeDeck = new List<ICard<Bitmap, string>>();
		}

		/// <summary>
		/// Removes and returns the top card from the active deck.
		/// </summary>
		/// <returns>The top <see cref="ICard{TImage, TValue}"/> from the active deck of cards.</returns>
		public ICard<Bitmap, string> GetTopCard()
		{
			ICard<Bitmap, string> topCard = _activeDeck[0];
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
