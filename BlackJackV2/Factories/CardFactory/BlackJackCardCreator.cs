// Project: BlackJackV2
// file: BlackJackV2/Factories/CardFactory/BlackJackCardCreator.cs

/// <summary>
///		A concrete product class, part of the Card Factory Pattern. Creates BlackJack Card objects
/// </summary>

using Avalonia.Media.Imaging;
using BlackJackV2.Models.Card;

namespace BlackJackV2.Factories.CardFactory
{
	internal class BlackJackCardCreator : ICardCreator<Bitmap, string>
	{
		public override ICard<Bitmap, string> CreateCard(Bitmap frontImage, Bitmap backImage, string value) 
			=> new BlackJackCard(frontImage, backImage, value);
	}
}
