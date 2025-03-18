using Avalonia.Media.Imaging;
using Avalonia.Platform;
using BlackJackV2.Models.CardFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackV2.Models.CardDeck
{
	/**
	 * This class sole responsability is to create the black jack card deck
	 * It creates a list of card objects with help from the card factory. 
	 **/

	internal class BlackJackDeckCreator
	{
		public List<ICard<Bitmap, Bitmap, string>> CreateDeck(ICardCreator<Bitmap, Bitmap, string> cardCreator)
		{
			List<ICard<Bitmap, Bitmap, string>> _cards = new List<ICard<Bitmap, Bitmap, string>>();

			string[] suites = { "Hearts", "Dimonds", "Clubs", "Spades" };
			int[] values = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 };

			Uri cardBackImageUri = new Uri("avares://BlackJackV2/Assets/Cards/Card_Back.png");
			Bitmap cardBackImage = new Bitmap(AssetLoader.Open(cardBackImageUri));


			foreach (string suite in suites)
			{
				foreach (int value in values)
				{
					Uri cardFrontImageUri = new Uri($"avares://BlackJackV2/Assets/Cards/{suite}_{value}.png");
					Bitmap cardFrontImage = new Bitmap(AssetLoader.Open(cardFrontImageUri));

					ICard<Bitmap, Bitmap, string> card = cardCreator.CreateCard(cardFrontImage, cardBackImage, $"{suite}_{value.ToString()}");

					_cards.Add(card);
				}
			}

			return _cards;
		}
	}
}
