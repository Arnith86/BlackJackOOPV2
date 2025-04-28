// Project: BlackJackV2
// file: BlackJackV2/Models/GameLogic/GameRuleServices/RoundEvaluator.cs

using BlackJackV2.Models.CardHand;
using BlackJackV2.Models.GameLogic.GameRuleServices.Interfaces;

namespace BlackJackV2.Models.GameLogic.GameRuleServices
{
	/// <summary>
	///	Provides functionality to evaluate the outcome of a Blackjack round between a player and the dealer.
	/// </summary>
	public class RoundEvaluator<TImage, TValue> : IRoundEvaluator<TImage, TValue>
	{
		private IBlackJackCardHand<TImage, TValue> _playerHand;
		private IBlackJackCardHand<TImage, TValue> _dealerHand;

		/// <summary>
		/// Evaluates the outcome of a Blackjack round between a player and the dealer based on their hands.
		/// </summary>
		/// <param name="playerHand">The player's <see cref="IBlackJackCardHand{TImage, TValue}"/> to evaluate.</param>
		/// <param name="dealerHand">The dealer's <see cref="IBlackJackCardHand{TImage, TValue}"/> to evaluate.</param>
		/// <returns>The result of the round as a <see cref="RoundResult"/>.</returns>
		public RoundResult EvaluateRound(IBlackJackCardHand<TImage, TValue> playerHand, IBlackJackCardHand<TImage, TValue> dealerHand) 
		{
			_playerHand = playerHand; 
			_dealerHand = dealerHand;

			// Both have black jack, its a tie 
			if (_playerHand.IsBlackJack && _dealerHand.IsBlackJack) return RoundResult.Push;

			// If only player has black jack, player wins
			if (_playerHand.IsBlackJack) return RoundResult.PlayerWinsBlackJack;

			// If only dealer has black jack, dealer wins
			if (_dealerHand.IsBlackJack) return RoundResult.DealerWinsBlackJack;

			// If player is busted, dealer wins
			if (_playerHand.IsBusted) return RoundResult.DealerWins;

			// If dealer is busted, player wins
			if (_dealerHand.IsBusted) return RoundResult.PlayerWins;

			// If player has higher value than dealer, player wins
			if (_playerHand.HandValue > _dealerHand.HandValue) return RoundResult.PlayerWins;

			// If dealer has higher value than player, dealer wins
			if (_playerHand.HandValue < _dealerHand.HandValue) return RoundResult.DealerWins;

			// If both have the same value, its a tie
			return RoundResult.Push;
		}
	}
}
