// Project: BlackJackV2
// file: BlackJackV2/Shared/Constants/BlackJackActions.cs 


/// <summary>
///		Contains possible actions in the blackjack game
/// </summary> 
namespace BlackJackV2.Shared.Constants
{
	public static class BlackJackActions
	{
		public enum PlayerActions
		{
			Hit,
			DoubleDown,
			Fold,
			Split
		}
	}
}
