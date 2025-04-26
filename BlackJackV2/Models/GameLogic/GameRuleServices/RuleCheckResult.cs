// Project: BlackJackV2
// file: BlackJackV2/Models/GameLogic/GameRuleServices/RulesCheckResult.cs

namespace BlackJackV2.Models.GameLogic.GameRuleServices
{
	/// <summary>
	/// Represents the result of a rule check in the game.
	/// </summary>
	public record RuleCheckResult(bool IsAllowed, string? Message = null)
	{

		/// <summary>
		/// Creates a new instance of <see cref="RuleCheckResult"/> indicating that the rule check was successful."/>
		/// </summary>
		/// <returns>A new instance of <see cref="RuleCheckResult"/> indicating that the rule check was successful.</returns>
		public static RuleCheckResult Allowed() => new RuleCheckResult(true);

		/// <summary>
		/// Creates a new instance of <see cref="RuleCheckResult"/> indicating that the rule check was denied, and has the option of assigning a message.
		/// </summary>
		/// <param name="message">The optional message assigned to the denied result.</param>
		/// <returns>A new instance of <see cref="RuleCheckResult"/> indicating that rule check was denied, and an optional message.</returns>
		public static RuleCheckResult Denied(string message) => new RuleCheckResult(false, message);
	}
}
