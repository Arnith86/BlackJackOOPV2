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

	internal class BlackJackDeckBuilder
	{
		public List<ICard<Bitmap, string>> CreateDeck(ICardCreator<Bitmap, string> cardCreator)
		{
			List<ICard<Bitmap, string>> _cards = new List<ICard<Bitmap, string>>();

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

					ICard<Bitmap, string> card = cardCreator.CreateCard(cardFrontImage, cardBackImage, $"{suite}_{value.ToString()}");

					_cards.Add(card);
				}
			}

			////// these are not important, only present to allow for continued testing 
			//Uri cardBackImageUri = new Uri("avares://BlackJackV2/Assets/Cards/Card_Back.png");
			//Bitmap cardBackImage = new Bitmap(AssetLoader.Open(cardBackImageUri));
			//Uri cardFrontImageUri = new Uri($"avares://BlackJackV2/Assets/Cards/Hearts_1.png");
			//Bitmap cardFrontImage = new Bitmap(AssetLoader.Open(cardFrontImageUri));

			//ICard<Bitmap, Bitmap, string> card7 = cardCreator.CreateCard(cardFrontImage, cardBackImage, "Hearts_11");
			//_cards.Add(card7);
			//ICard<Bitmap, Bitmap, string> card6 = cardCreator.CreateCard(cardFrontImage, cardBackImage, "Hearts_6");
			//_cards.Add(card6);
			//ICard<Bitmap, Bitmap, string> card1 = cardCreator.CreateCard(cardFrontImage, cardBackImage, "Hearts_1");
			//_cards.Add(card1);
			//ICard<Bitmap, Bitmap, string> card2 = cardCreator.CreateCard(cardFrontImage, cardBackImage, "Clubs_1");
			//_cards.Add(card2);
			////ICard<Bitmap, Bitmap, string> card3 = cardCreator.CreateCard(cardFrontImage, cardBackImage, "Hearts_12");
			////_cards.Add(card3);
			////ICard<Bitmap, Bitmap, string> card4 = cardCreator.CreateCard(cardFrontImage, cardBackImage, "Hearts_13");
			////_cards.Add(card4);
			////ICard<Bitmap, Bitmap, string> card5 = cardCreator.CreateCard(cardFrontImage, cardBackImage, "Hearts_5");
			////_cards.Add(card5);

			return _cards;
		}
	}
}
