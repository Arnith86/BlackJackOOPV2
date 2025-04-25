// Project: BlackJackV2
// file: BlackJackV2/Shared/Utilities/CardToValueUtility.cs

using Avalonia.Media.Imaging;
using BlackJackV2.Models.Card;
using System.Linq;

namespace BlackJackV2.Shared.UtilityClasses
{
	/// <summary>
	/// This utility class is used for extracting a card numeric and suit representation.
	/// </summary>
	public static class CardToValueUtility
	{
		/// <summary>
		/// Extracts the numerical value of the card.
		/// </summary>
		/// <param name="card">The card from which to extract the value.</param>
		/// <returns></returns>
		public static string GetNumericCardValue(ICard<Bitmap, string> card) => card.Value.Split('_').Last();
		
		/// <summary>
		/// Extracts the suite value of the card.
		/// </summary>
		/// <param name="card">The card from which to extract the suite.</param>
		/// <returns></returns>
		public static string GetCardSuit(ICard<Bitmap, string> card) => card.Value.Split('_').First();
	}
}
