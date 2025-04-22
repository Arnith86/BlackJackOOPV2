// Project: BlackJackV2
// file: BlackJackV2/Models/CardDeck/BlackJackCardDeckCreator.cs

using Avalonia.Media.Imaging;
using Avalonia.Platform;
using BlackJackV2.Factories.CardFactory;
using BlackJackV2.Models.Card;
using BlackJackV2.Models.CardDeck;
using System.Collections.Generic;
using System;

namespace BlackJackV2.Factories.CardDeckFactory
{
	/// <summary>
	///	Concrete creator class for the Card Deck Factory pattern.
	///	Responsible for creating a fully initialized <see cref="BlackJackCardDeck"/> for use in a blackjack game.
	///	
	/// The deck is constructed using a provided <see cref="CardCreator{TImage, TValue}"/> to generate all 52 cards.
	/// (13 cards across 4 suites), each with a front and back image.
	/// </summary>
	internal class BlackJackCardDeckCreator : CardDeckCreator<Bitmap, string>
	{
		private CardCreator<Bitmap, string> _cardCreator;
		
		public BlackJackCardDeckCreator(CardCreator<Bitmap, string> cardCreator) 
		{
			_cardCreator = cardCreator;
		}

		/// <summary>
		/// Builds and returns a complete BlackJack card deck, with face and back images for each card.
		/// </summary>
		/// <returns>
		/// Fully populated <see cref="BlackJackCardDeck"/> representing a standard 52-card Blackjack deck. 
		/// </returns>
		public override ICardDeck<Bitmap, string> CreateDeck()
		{
			List<ICard<Bitmap, string>> cards = new List<ICard<Bitmap, string>>();

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

					ICard<Bitmap, string> card = _cardCreator.CreateCard(cardFrontImage, cardBackImage, $"{suite}_{value.ToString()}");

					cards.Add(card);
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

			return new BlackJackCardDeck(cards);
		}
	}
}
