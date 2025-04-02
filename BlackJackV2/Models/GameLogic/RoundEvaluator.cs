using Avalonia.Media.Imaging;
using BlackJackV2.Models.CardHand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackV2.Models.GameLogic
{
	public class RoundEvaluator
	{
		/**
		 * Evaluates the round and determines the winner
		 **/
		public enum RoundResult
		{
			PlayerWinsBlackJack,
			DealerWinsBlackJack,
			PlayerWins,
			DealerWins,
			Push

		}
		BlackJackCardHand _playerHand;
		BlackJackCardHand _dealerHand;

		public RoundResult EvaluateRound(ICardHand<Bitmap, string> playerHand, ICardHand<Bitmap, string> dealerHand) 
		{
			_playerHand = playerHand as BlackJackCardHand;
			_dealerHand = dealerHand as BlackJackCardHand;

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
