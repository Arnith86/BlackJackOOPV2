// Project: BlackJackV2
// file: BlackJackV2/Shared/Utilities/CardToValueUtility.cs

using Avalonia.Media.Imaging;
using BlackJackV2.Models.Card;
using System.Linq;

namespace BlackJackV2.Shared.UtilityClasses
{
	public static class CardToValueUtility
	{
		public static string GetNumericCardValue(ICard<Bitmap, string> card) => card.Value.Split('_').Last();
		public static string GetCardSuit(ICard<Bitmap, string> card) => card.Value.Split('_').First();
	}
}
