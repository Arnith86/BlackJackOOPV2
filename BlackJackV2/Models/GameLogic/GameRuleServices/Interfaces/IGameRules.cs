// Project: BlackJackV2
// file: BlackJackV2/Models/GameLogic/GameRuleServices/Interfaces/IGameRules.cs

using BlackJackV2.Models.Player;
using BlackJackV2.Shared.Constants;

namespace BlackJackV2.Models.GameLogic.GameRuleServices.Interfaces
{
	/// <summary>
	/// Defines rule-checking services used to validate whether specific player actions 
	/// in the game of Blackjack (e.g., splitting or doubling down) are allowed based on current game state.
	/// </summary>
	public interface IGameRules<TImage, TValue>
	{
		/// <summary>
		/// Checks if the player is allowed to fold their hand based on the current game state. (Must have one drawn card.)
		/// </summary>
		/// <param name="player">The player attempting to fold.</param>
		/// <param name="primaryOrSplit">Specifies which hand is being checked (e.g., primary or split).</param>
		/// <returns>A result indicating whether the action is allowed, along with an optional message.</returns>
		public RuleCheckResult CanFold(IPlayer<TImage, TValue> player, HandOwners.HandOwner primaryOrSplit);

		/// <summary>
		/// Checks if the player is allowed to double down based on their current hand and game state.
		/// </summary>
		/// <param name="player">The player attempting to double down.</param>
		/// <param name="primaryOrSplit">Specifies which hand is being checked (e.g., primary or split).</param>
		/// <returns>A result indicating whether the action is allowed, along with an optional message.</returns>
		public RuleCheckResult CanDoubleDown(IPlayer<TImage, TValue> player, HandOwners.HandOwner primaryOrSplit);

		/// <summary>
		/// Checks if the player is allowed to split their hand based on the rules of Blackjack.
		/// </summary>
		/// <param name="player">The player attempting to split.</param>
		/// <returns>A result indicating whether the action is allowed, along with an optional message.</returns>
		public RuleCheckResult CanSplit(IPlayer<TImage, TValue> player);


		/// <summary>
		/// Validates if a player has enough funds to place the initial bet.
		/// </summary>
		/// <param name="player">The player making the bet.</param>
		/// <param name="betAmount">The amount the player wants to bet.</param>
		/// <returns>A RuleCheckResult indicating if the bet is allowed.</returns>
		public RuleCheckResult CanPlaceInitialBet(IPlayer<TImage, TValue> player, int betAmount);
	}
}
