// Project: BlackJackV2
// file: BlackJackV2/Factories/CardHandFactory/BlackJackCardHandCreator.cs

using BlackJackV2.Models.CardHand;

namespace BlackJackV2.Factories.CardHandFactory
{
	/// <summary>
	///	Concrete creator class for the CardHand factory pattern, which provides a way of creating a collection of cards representing a hand in BlackJack.
	///	This class creates a fully initialized <see cref="BlackJackCardHand"/> object.
	/// </summary>
	/// <remarks> Related files <see cref="BlackJackV2.Models.CardHand"/></remarks>
	public class BlackJackCardHandCreator<TImage, TValue> : CardHandCreator<TImage, TValue>
	{
		/// <summary>
		///	Creates a new BlackJack hand, which represents a collection of cards that a player or dealer can hold. 
		///	This hand is fully initialized for use in a BlackJack game.
		/// </summary>
		/// <returns>
		///	An instance of <see cref="BlackJackCardHand"/> representing a fully initialized hand of cards used in blackJack. 
		///	</returns>
		public override IBlackJackCardHand<TImage, TValue> CreateCardHand()
		{
			return new BlackJackCardHand<TImage, TValue>();
		}
	}
}
