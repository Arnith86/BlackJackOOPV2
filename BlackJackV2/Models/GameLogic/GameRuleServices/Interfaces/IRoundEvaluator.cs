// Project: BlackJackV2
// file: BlackJackV2/Models/GameLogic/GameRuleServices/Interfaces/IRoundEvaluator.cs

using BlackJackV2.Models.CardHand;
using BlackJackV2.Shared.Constants;

namespace BlackJackV2.Models.GameLogic.GameRuleServices.Interfaces
{
	/// <summary>
	/// Defines functionality to evaluate the outcome of a Blackjack round between a player and the dealer.
	/// </summary>
	public interface IRoundEvaluator<TImage, TValue>
	{
		/// <summary>
		/// Evaluates the outcome of a Blackjack round between a player and the dealer based on their hands.
		/// </summary>
		/// <param name="playerHand">The player's <see cref="IBlackJackCardHand{TImage, TValue}"/> to evaluate.</param>
		/// <param name="dealerHand">The dealer's <see cref="IBlackJackCardHand{TImage, TValue}"/> to evaluate.</param>
		/// <returns>The result of the round as a <see cref="RoundResult"/>.</returns>
		BlackJackRoundResult.RoundResult EvaluateRound(IBlackJackCardHand<TImage, TValue> playerHand, IBlackJackCardHand<TImage, TValue> dealerHand);
	}
	
	//public enum RoundResult
	//{
	//	PlayerWinsBlackJack,
	//	DealerWinsBlackJack,
	//	PlayerWins,
	//	DealerWins,
	//	Push
	//}
}
