// Project: BlackJackV2
// file: BlackJackV2/Factories/CardFactory/CardCreator.cs

using BlackJackV2.Models.Card;

namespace BlackJackV2.Factories.CardFactory
{
	/// <summary>
	///	Defines an abstract for a creator class responsible for creating <see cref="ICard{TImage, TValue}"/> objects.
	///	This is part of the Card factory pattern which provides a way to create card objects.
	/// </summary>
	/// <remarks> Related files <see cref="BlackJackV2.Models.Card"/></remarks>
	public abstract class CardCreator<TImage, TValue>
	{
		/// <summary>
		///	Creates a card using the provided front and back images, and value, and returns an initialized card.
		/// </summary>
		/// <param name="frontImage"> The image representing the front of the card. </param>
		/// <param name="backImage"> The image representing the back of the card. </param>
		/// <param name="value"> The value associated with the card. Could be a string or number depending on the game. </param>
		/// <returns>
		///	An instance of <see cref="ICard{TImage, TValue}"/> that represent the created card.
		/// </returns>
		public abstract ICard<TImage, TValue> CreateCard(TImage frontImage, TImage backImage, TValue value);
	}
}
