// Project: BlackJackV2
// file: BlackJackV2/Shared/Constants/HandOwners.cs



/// <summary>
///		Contains possible hands in the blackjack game
/// </summary>
namespace BlackJackV2.Shared.Constants
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
