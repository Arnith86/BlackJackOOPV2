// Project: BlackJackV2
// file: BlackJackV2/Shared/Constants/RoundResults.cs 

namespace BlackJackV2.Shared.Constants
{
	/// <summary>
	///	Contains possible round results a round can end in the blackjack game
	/// </summary> 
	public static class BlackJackRoundResult
	{
		public enum RoundResult
		{
			PlayerWinsBlackJack,
			DealerWinsBlackJack,
			PlayerWins,
			DealerWins,
			Push
		}
	}
}
