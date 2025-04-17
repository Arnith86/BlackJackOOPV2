// Project: BlackJackV2
// file: BlackJackV2/Models/CardDeck/BlackJackCardDeck.cs

/// <summary>
///
///		The class simulates a black jack card deck and contains the logic related to the Card Deck. 
/// 
///		List<ICard<Bitmap, Bitmap, string>> _originalDeck	:		An undisturbed version of the card deck
///		List<ICard<Bitmap, Bitmap, string>> _activeDeck		: 		The card deck in active us, can be shuffled and drawn from
///
///		ICard<Bitmap, string>	GetTopCard()	: Retrieves top card of the active card deck.
///		void					ShuffleDeck()	: Shuffles the active card deck.
///		
/// </summary>

using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Media.Imaging;

namespace BlackJackV2.Models.CardDeck
{
	public class BlackJackCardDeck : ICardDeck<Bitmap, string>
	{

		List<ICard<Bitmap, string>> _originalDeck;
		List<ICard<Bitmap, string>> _activeDeck;

		public BlackJackCardDeck(List<ICard<Bitmap, string>> cardDeck)
		{
			_originalDeck = cardDeck;
			_activeDeck = new List<ICard<Bitmap, string>>();
		}

		public ICard<Bitmap, string> GetTopCard()
		{
			ICard<Bitmap, string> topCard = _activeDeck[0];
			_activeDeck.RemoveAt(0);

			return topCard;
		}

		public void ShuffleDeck()
		{
			Random random = new Random();
			_activeDeck.Clear();
			_activeDeck = _originalDeck.OrderBy(x => random.Next()).ToList();
		}
	}
}
