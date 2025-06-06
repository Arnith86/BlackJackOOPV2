﻿// Project: BlackJackV2
// file: BlackJackV2/Factories/CardDeckFactory/BlackJackDeckBuilder.cs

using BlackJackV2.Models.CardDeck;

namespace BlackJackV2.Factories.CardDeckFactory
{
	/// <summary>
	/// Defines the interface for a creator class responsible for creating <see cref="ICardDeck{TImage, TValue}"/> objects.
	/// This is part of the Card Deck factory pattern used to build specific deck types (e.g., Blackjack).
	/// </summary>
	/// <remarks> Related files <see cref="BlackJackV2.Models.CardDeck"/></remarks>
	public abstract class CardDeckCreator<TImage, TValue>
	{
		/// <summary>
		///	Creates and returns a new card deck instance.
		/// </summary>
		/// <returns> An inialized card deck of type <see cref="ICardDeck{TImage, TValue}"/>.</returns>
		public abstract ICardDeck<TImage, TValue> CreateDeck();
	}
}
