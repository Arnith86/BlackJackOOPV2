// Project: BlackJackV2
// file: BlackJackV2/Factories/CardHandFactory/BlackJackCardHandCreator.cs

using Avalonia.Media.Imaging;
using BlackJackV2.Models.CardHand;

/// <summary>
///		Concrete creator class for the CardHand factory pattern, which provides a way of creating a collection of cards representing a hand in BlackJack.
///		This class creates a fully initialized <see cref="BlackJackCardHand"/> object.
/// </summary>

namespace BlackJackV2.Factories.CardHandFactory
{
	internal class BlackJackCardHandCreator : CardHandCreator<Bitmap, string>
	{
		/// <summary>
		///		Creates a new BlackJack hand, which represents a collection of cards that a player or dealer can hold. 
		///		This hand is fully initialized for use in a BlackJack game.
		/// </summary>
		/// <returns>
		///		An instance of <see cref="BlackJackCardHand"/> representing a fully initialized hand of cards used in blackJack. 
		///	</returns>
		public override IBlackJackCardHand<Bitmap, string> CreateCardHand()
		{
			return new BlackJackCardHand();
		}
	}
}
