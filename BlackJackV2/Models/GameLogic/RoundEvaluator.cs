// Project: BlackJackV2
// file: BlackJackV2/Models/GameLogic/RoundEvaluator.cs

/// <summary>
///			
///		This class evaluates a single player hand against the dealer hand, and determines the winner.
///		
///		enum				RoundResult	: Possible results of the round. 
///		BlackJackCardHand	_playerHand	: Player hand to evaluate.
///		BlackJackCardHand	_dealerHand	: Dealer hand to evaluate.
///		
///		EvaluateRound()		: Evaluates the round and determines the winner.
/// </summary>

using BlackJackV2.Models.CardHand;

namespace BlackJackV2.Models.GameLogic
{
	public class RoundEvaluator<TImage, TValue>
	{
		public enum RoundResult
		{
			PlayerWinsBlackJack,
			DealerWinsBlackJack,
			PlayerWins,
			DealerWins,
			Push

		}
		
		IBlackJackCardHand<TImage, TValue> _playerHand;
		IBlackJackCardHand<TImage, TValue> _dealerHand;

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
