using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using BlackJackV2.Models.CardFactory;
using DynamicData;

namespace BlackJackV2.Models.CardDeck
{
	/** 
	 * The class simulates a black jack card deck and contains the logic related to the Card Deck. 
	 * 
	 * List<ICard<Bitmap, Bitmap, string>> _originalDeck:		An undisturbed version of the card deck
	 * List<ICard<Bitmap, Bitmap, string>> _activeDeck: 		The card deck in active us, can be shuffled and drawn from
	 * */

	internal class BlackJackCardDeck : ICardDeck<Bitmap, Bitmap, string>
	{

		List<ICard<Bitmap, Bitmap, string>> _originalDeck;
		List<ICard<Bitmap, Bitmap, string>> _activeDeck;

		public BlackJackCardDeck(List<ICard<Bitmap, Bitmap, string>> cardDeck)
		{
			_originalDeck = cardDeck;
			_activeDeck = new List<ICard<Bitmap, Bitmap, string>>();
		}

		public ICard<Bitmap, Bitmap, string> GetTopCard()
		{
			ICard<Bitmap, Bitmap, string> topCard = _activeDeck[0];
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
