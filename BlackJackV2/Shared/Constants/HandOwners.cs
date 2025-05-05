// Project: BlackJackV2
// file: BlackJackV2/Shared/Constants/HandOwners.cs

namespace BlackJackV2.Shared.Constants
{
	/// <summary>
	///	Contains possible hands in the blackjack game
	/// </summary>
	public static class HandOwners
	{
		public enum HandOwner
		{
			Primary,
			Split,
			Dealer,
			Player
		}
	}
}
