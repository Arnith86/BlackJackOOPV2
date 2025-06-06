﻿// Project: BlackJackV2
// file: BlackJackV2/Factories/CardFactory/BlackJackCardCreator.cs

using BlackJackV2.Models.Card;

namespace BlackJackV2.Factories.CardFactory
{
	/// <summary>
	///	Concrete creator class for the Card factory patter, specifically for BlackJack.
	///	Responsible for creating a fully initialized <see cref="BlackJackCard"/> object for use in the game.
	/// </summary>
	/// <remarks> Related files <see cref="BlackJackV2.Models.Card"/></remarks>
	public class BlackJackCardCreator<TImage, TValue> : CardCreator<TImage, TValue>
	{
		/// <summary>
		///	Creates a card used in BlackJack, using the provided front and back images, and value, and returns an initialized card.
		/// </summary>
		/// <param name="frontImage">	The image representing the front of the card. </param>
		/// <param name="backImage">	The image representing the back of the card. </param>
		/// <param name="value">		The value associated with the card. A string that follows this template "Suite_Value"  (e.g., "Hearts_2") .  
		///								Suits: Hearts, Diamonds, Clubs, Spades
		///								Values: 1 - 13 (11 = Knight, 12 = Queen, 13 = King)
		///	</param>
		/// <returns>
		///	An instance of <see cref="BlackJackCard"/> that represent the created card.
		/// </returns>
		public override ICard<TImage, TValue> CreateCard(TImage frontImage, TImage backImage, TValue value) 
			=> new BlackJackCard<TImage, TValue>(frontImage, backImage, value);
	}
}
