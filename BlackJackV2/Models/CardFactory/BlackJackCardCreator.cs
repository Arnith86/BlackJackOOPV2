// Project: BlackJackV2
// file: BlackJackV2/Models/CardFactory/BlackJackCardCreator.cs

/// <summary>
///		A concrete product class, part of the Card Factory Pattern. Creates BlackJack Card objects
/// </summary>

using Avalonia.Media.Imaging;

namespace BlackJackV2.Models.CardFactory
{
	internal class BlackJackCardCreator : ICardCreator<Bitmap, string>
	{
		public override ICard<Bitmap, string> CreateCard(Bitmap frontImage, Bitmap backImage, string value)
		{
			return new BlackJackCard(frontImage, backImage, value);
		}
	}
}
