using Avalonia.Media.Imaging;
using BlackJackV2.Models.Card;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackV2.UtilityClasses
{
	public static class CardToValueUtility
	{
		public static string GetNumericCardValue(ICard<Bitmap, string> card) => card.Value.Split('_').Last();
		public static string GetCardSuit(ICard<Bitmap, string> card) => card.Value.Split('_').First();
	}
}
