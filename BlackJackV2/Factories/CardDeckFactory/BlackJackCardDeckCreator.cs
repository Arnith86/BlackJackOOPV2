// Project: BlackJackV2
// file: BlackJackV2/Models/CardDeck/BlackJackCardDeckCreator.cs

using BlackJackV2.Factories.CardFactory;
using BlackJackV2.Models.Card;
using BlackJackV2.Models.CardDeck;
using System.Collections.Generic;
using System.Linq;
using BlackJackV2.Shared.Utilities.ImageLoader;

namespace BlackJackV2.Factories.CardDeckFactory
{
	/// <summary>
	///	Concrete creator class for the Card Deck Factory pattern.
	///	Responsible for creating a fully initialized <see cref="BlackJackCardDeck"/> for use in a blackjack game.
	///	
	/// The deck is constructed using a provided <see cref="CardCreator{TImage, TValue}"/> to generate all 52 cards.
	/// (13 cards across 4 suites), each with a front and back image.
	/// </summary>
	/// <remarks> Related files <see cref="BlackJackV2.Models.CardDeck"/></remarks>
	internal class BlackJackCardDeckCreator<TImage, TValue> : CardDeckCreator<TImage, TValue>
	{
		private readonly CardCreator<TImage, TValue> _cardCreator;
		private readonly IImageLoader<TImage> _imageLoader;

		/// <summary>
		/// Initializes a new instance of the <see cref="BlackJackCardDeckCreator"/> class.
		/// </summary>
		/// <param name="cardCreator">Creator for individual cards, used to generate the deck.</param>
		/// <param name="imageLoader">Loader for card images, used to load front and back images.</param>
		public BlackJackCardDeckCreator(CardCreator<TImage, TValue> cardCreator, IImageLoader<TImage> imageLoader) 
		{
			_cardCreator = cardCreator;
			_imageLoader = imageLoader;
		}

		/// <summary>
		/// Builds and returns a complete BlackJack card deck, with face and back images for each card.
		/// </summary>
		/// <returns>
		/// Fully populated <see cref="BlackJackCardDeck"/> representing a standard 52-card Blackjack deck. 
		/// </returns>
		public override ICardDeck<TImage, TValue> CreateDeck()
		{
			List<ICard<TImage, TValue>> cards = new List<ICard<TImage, TValue>>();

			string[] suites = { "Hearts", "Dimonds", "Clubs", "Spades" };
			int[] values = Enumerable.Range(1, 13).ToArray();

			TImage cardBackImage = _imageLoader.Load("avares://BlackJackV2/Assets/Cards/Card_Back.png");

			foreach (string suite in suites)
			{
				foreach (int value in values)
				{
					TImage cardFrontImage = _imageLoader.Load($"avares://BlackJackV2/Assets/Cards/{suite}_{value}.png");
					TValue lable = (TValue)(object)$"{suite}_{value.ToString()}";

					ICard<TImage, TValue> card = _cardCreator.CreateCard(cardFrontImage, cardBackImage, lable);

					cards.Add(card);
				}
			}


			////// these are not important, only present to allow for continued testing 
			//TImage cardBackImage = _imageLoader.Load("avares://BlackJackV2/Assets/Cards/Card_Back.png");
			//TImage cardFrontImage = _imageLoader.Load($"avares://BlackJackV2/Assets/Cards/Hearts_1.png");

			//ICard<TImage, TValue> card7 = _cardCreator.CreateCard(cardFrontImage, cardBackImage, (TValue)(object)"Hearts_1");
			//cards.Add(card7);
			//ICard<TImage, TValue> card6 = _cardCreator.CreateCard(cardFrontImage, cardBackImage, (TValue)(object)"Hearts_1");
			//cards.Add(card6);
			//ICard<TImage, TValue> card1 = _cardCreator.CreateCard(cardFrontImage, cardBackImage, (TValue)(object)"Hearts_1");
			//cards.Add(card1);
			//ICard<TImage, TValue> card2 = _cardCreator.CreateCard(cardFrontImage, cardBackImage, (TValue)(object)"Clubs_1");
			//cards.Add(card2);
			//ICard<TImage, TValue> card3 = _cardCreator.CreateCard(cardFrontImage, cardBackImage, (TValue)(object)"Spades_1");
			//cards.Add(card3);
			//ICard<TImage, TValue> card4 = _cardCreator.CreateCard(cardFrontImage, cardBackImage, (TValue)(object)"Dimonds_1");
			//cards.Add(card4);
			//ICard<TImage, TValue> card5 = _cardCreator.CreateCard(cardFrontImage, cardBackImage, (TValue)(object)"Hearts_1");
			//cards.Add(card5);
			//ICard<TImage, TValue> card8 = _cardCreator.CreateCard(cardFrontImage, cardBackImage, (TValue)(object)"Hearts_1");
			//cards.Add(card8);
			//ICard<TImage, TValue> card9 = _cardCreator.CreateCard(cardFrontImage, cardBackImage, (TValue)(object)"Hearts_1");
			//cards.Add(card9);

			return new BlackJackCardDeck<TImage, TValue>(cards);
		}
	}
}
