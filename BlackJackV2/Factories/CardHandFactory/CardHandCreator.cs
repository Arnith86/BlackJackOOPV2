// Project: BlackJackV2
// file: BlackJackV2/Factories/CardHandFactory/CardHandCreator.cs

using BlackJackV2.Models.CardHand;

/// <summary>
///		Defines an abstract for a creator class responsible for creating <see cref="IBlackJackCardHand{TImage, TValue}"/> objects.
///		This is part of the CardHand factory pattern, which provides a way of creating a collection of cards representiing a hand in BlackJack.
/// </summary>

namespace BlackJackV2.Factories.CardHandFactory
{
	public abstract class CardHandCreator<TImage, TValue>
	{
		/// <summary>
		///		Creates a new BlackJack hand, which represents a collection of cards that a player or dealer can hold.  
		/// </summary>
		/// <returns>
		///		An instance of <see cref="IBlackJackCardHand{TImage, TValue}"/> representing a hand of cards. 
		///	</returns>
		public abstract IBlackJackCardHand<TImage, TValue> CreateCardHand();
	}
}
