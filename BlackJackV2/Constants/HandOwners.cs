// Project: BlackJackV2
// file: BlackJackV2/Constants/HandOwners.cs

/// <summary>
///		Contains possible hands in the blackjack game
/// </summary>


namespace BlackJackV2.Constants
{
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
