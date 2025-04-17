// Project: BlackJackV2
// file: BlackJackV2/Constants/BlackJackActions.cs 

/// <summary>
///		Contains possible actions in the blackjack game
///		
///		enum PlayerActions	: Possible actions in this black jack game.
///		
/// </summary> 

namespace BlackJackV2.Constants
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
