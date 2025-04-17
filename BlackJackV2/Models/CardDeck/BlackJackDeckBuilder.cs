// Project: BlackJackV2
// file: BlackJackV2/Models/CardDeck/BlackJackDeckBuilder.cs

/// <summary>
/// 
///		This class sole responsability is to create the black jack card deck
///		It creates a list of card objects with help from the card factory. 
///		
///		List<ICard<Bitmap, string>> CreateDeck()	:	returns a list of cards.
/// 
/// </summary>

using Avalonia.Media.Imaging;
using Avalonia.Platform;
using BlackJackV2.Models.CardFactory;
using System;
using System.Collections.Generic;


namespace BlackJackV2.Models.CardDeck
{
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

			//ICard<Bitmap, string> card7 = cardCreator.CreateCard(cardFrontImage, cardBackImage, "Hearts_1");
			//_cards.Add(card7);
			//ICard<Bitmap, string> card6 = cardCreator.CreateCard(cardFrontImage, cardBackImage, "Hearts_1");
			//_cards.Add(card6);
			//ICard<Bitmap, string> card1 = cardCreator.CreateCard(cardFrontImage, cardBackImage, "Hearts_1");
			//_cards.Add(card1);
			//ICard<Bitmap, string> card2 = cardCreator.CreateCard(cardFrontImage, cardBackImage, "Clubs_1");
			//_cards.Add(card2);
			//ICard<Bitmap, string> card3 = cardCreator.CreateCard(cardFrontImage, cardBackImage, "Spades_1");
			//_cards.Add(card3);
			//ICard<Bitmap, string> card4 = cardCreator.CreateCard(cardFrontImage, cardBackImage, "Dimonds_1");
			//_cards.Add(card4);
			//ICard<Bitmap, string> card5 = cardCreator.CreateCard(cardFrontImage, cardBackImage, "Hearts_1");
			//_cards.Add(card5);
			//ICard<Bitmap, string> card8 = cardCreator.CreateCard(cardFrontImage, cardBackImage, "Hearts_1");
			//_cards.Add(card8);
			//ICard<Bitmap, string> card9 = cardCreator.CreateCard(cardFrontImage, cardBackImage, "Hearts_1");
			//_cards.Add(card9);

			return _cards;
		}
	}
}
