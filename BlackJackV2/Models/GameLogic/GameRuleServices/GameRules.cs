// Project: BlackJackV2
// file: BlackJackV2/Models/GameLogic/GameRuleServices/GameRules.cs

using BlackJackV2.Models.GameLogic.GameRuleServices.Interfaces;
using BlackJackV2.Models.Player;
using BlackJackV2.Shared.Constants;
using BlackJackV2.Shared.UtilityClasses;

namespace BlackJackV2.Models.GameLogic.GameRuleServices
{
	/// <summary>
	/// Provides implementations for validating Blackjack gameplay rules, such as whether 
	/// a player can split their hand or double down based on their cards and available funds.
	/// </summary>
	public class GameRules<TImage, TValue> : IGameRules<TImage, TValue>
	{
		/// <summary>
		/// Validates whether the player is allowed to double down on the specified hand.
		/// </summary>
		/// <param name="player">The player attempting to double down.</param>
		/// <param name="whichHand">Specifies which hand is being evaluated.</param>
		/// <returns>A result indicating whether the action is allowed.</returns>
		public RuleCheckResult CanDoubleDown(IPlayer<TImage, TValue> player, HandOwners.HandOwner whichHand)
		{
			var hand = player.Hands.GetCardHand(whichHand);
			int bet = player.Hands.GetBetFromHand(whichHand);

			if (!player.EnoughFundsForBet(bet))
				return RuleCheckResult.Denied("You do not have enough funds to double down.");

			if (hand.Hand.Count != 2)
				return RuleCheckResult.Denied("You can only double down if you have exactly two cards.");
			
			return RuleCheckResult.Allowed();
		}

		/// <summary>
		/// Validates whether the player is allowed to split their hand.
		/// The player must have exactly two cards of the same value and enough funds to cover a second bet.
		/// </summary>
		/// <param name="player">The player attempting to split.</param>
		/// <returns>A result indicating whether the action is allowed and a message if it is denied.</returns>
		public RuleCheckResult CanSplit(IPlayer<TImage, TValue> player)
		{
			var hand = player.Hands.PrimaryCardHand.Hand;
			var splitHand = player.Hands.SplitCardHand.Hand;

			var cardValue1 = CardToValueUtility<TImage, TValue>.GetNumericCardValue(hand[0]);
			var cardValue2 = CardToValueUtility<TImage, TValue>.GetNumericCardValue(hand[1]);

			var bet = player.Hands.GetBetFromHand(HandOwners.HandOwner.Primary);

			if (hand.Count != 2)
				return RuleCheckResult.Denied("You can only split if you have exactly two cards.");
			
			if (!cardValue1.Equals(cardValue2))
				return RuleCheckResult.Denied("you can only split if the two cards have the same value.");
			
			if (!player.EnoughFundsForBet(bet))
				return RuleCheckResult.Denied("You do not have enough funds to split your hand.");

			if (splitHand.Count > 0)
				return RuleCheckResult.Denied("Only one split is allowed.");

			return RuleCheckResult.Allowed();

		}

		/// <summary>
		/// Validates if a player has enough funds to place the initial bet.
		/// </summary>
		/// <param name="player">The player making the bet.</param>
		/// <param name="betAmount">The amount the player wants to bet.</param>
		/// <returns>A RuleCheckResult indicating if the bet is allowed.</returns>
		public RuleCheckResult CanPlaceInitialBet(IPlayer<TImage, TValue> player, int betAmount)
		{
			if (betAmount < 0 || betAmount > 10)
				return RuleCheckResult.Denied("The bet amount must be between 0 and 10");

			if (!player.EnoughFundsForBet(betAmount))
				return RuleCheckResult.Denied("You do not have enough funds to place this bet.");

			return RuleCheckResult.Allowed();
		}
	}
}
