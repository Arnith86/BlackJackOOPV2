// Project: BlackJackV2
// file: BlackJackV2/Shared/Constants/BlackJackActions.cs 


namespace BlackJackV2.Shared.Constants
{
	/// <summary>
	///	Contains possible actions in the blackjack game
	/// </summary> 
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
