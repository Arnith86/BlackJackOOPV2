// Project: BlackJackV2
// file: BlackJackV2/Factories/CardHandFactory/BlackJackCardHandCreator.cs

/// <summary>
///		Part of CardHandFactory pattern
///		Concreat product for CardHand creation.
///		
///		BlackJackCardHand IBlackJackCardHand()	:	Returns a new BlackJackCardHand object 
///		
/// </summary>

using Avalonia.Media.Imaging;
using BlackJackV2.Models.CardHand;

namespace BlackJackV2.Factories.CardHandFactory
{
	internal class BlackJackCardHandCreator : ICardHandCreator<Bitmap, string>
	{
		public override IBlackJackCardHand<Bitmap, string> CreateCardHand()
		{
			return new BlackJackCardHand();
		}
	}
}
