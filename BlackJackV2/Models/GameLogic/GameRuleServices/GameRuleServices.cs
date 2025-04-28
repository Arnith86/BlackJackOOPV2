// Project: BlackJackV2
// file: BlackJackV2/Models/GameLogic/GameRuleServices/GameRuleServices.cs

using BlackJackV2.Models.GameLogic.GameRuleServices.Interfaces;

namespace BlackJackV2.Models.GameLogic.GameRuleServices
{
	/// <summary>
	/// Represents a container for core game rule services used in BlackJack, 
	/// bundling the evaluation logic and the general game rules into a single unit.
	/// </summary>
	/// <typeparam name="TImage">The type used for representing card images.</typeparam>
	/// <typeparam name="TValue">The type used for representing card values.</typeparam>
	/// <param name="GameRules">
	/// Defines the general rules of the BlackJack game, such as win conditions, bust conditions, and value evaluations.
	/// </param>
	/// <param name="RoundEvaluator">
	/// Provides the logic for evaluating the outcome of a round, determining winners, losers, and ties.
	/// </param>
	public record GameRuleServices<TImage, TValue>(IGameRules<TImage, TValue> GameRules, IRoundEvaluator<TImage, TValue> RoundEvaluator);
}
