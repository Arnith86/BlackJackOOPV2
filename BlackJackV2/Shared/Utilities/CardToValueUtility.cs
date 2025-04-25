// Project: BlackJackV2
// file: BlackJackV2/Shared/Utilities/CardToValueUtility.cs

using BlackJackV2.Models.Card;
using System.Linq;

namespace BlackJackV2.Shared.UtilityClasses
{
	/// <summary>
	/// This utility class is used for extracting a card numeric and suit representation.
	/// </summary>
	public static class CardToValueUtility<TImage, TValue>
	{
		/// <summary>
		/// Extracts the numerical value of the card.
		/// </summary>
		/// <param name="card">The card from which to extract the value.</param>
		/// <returns></returns>
		public static string GetNumericCardValue(ICard<TImage, TValue> card) => card.Value.ToString().Split('_').Last();
		
		/// <summary>
		/// Extracts the suite value of the card.
		/// </summary>
		/// <param name="card">The card from which to extract the suite.</param>
		/// <returns></returns>
		public static string GetCardSuit(ICard<TImage, TValue> card) => card.Value.ToString().Split('_').First();
	}
}
