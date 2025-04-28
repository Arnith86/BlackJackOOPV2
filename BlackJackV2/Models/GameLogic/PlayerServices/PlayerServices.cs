// Project: BlackJackV2
// file: BlackJackV2/Models/GameLogic/PlayerServices/PlayerServices.cs

namespace BlackJackV2.Models.GameLogic.PlayerServices
{
	/// <summary>
	/// Represents a container for player-related services in BlackJack, 
	/// bundling player actions and round management into a single unit.
	/// </summary>
	/// <typeparam name="TImage">The type used for representing card images.</typeparam>
	/// <typeparam name="TValue">The type used for representing card values.</typeparam>
	/// <param name="PlayerAction">
	/// Provides the logic for handling player actions such as hit, stand, split, and fold.
	/// </param>
	/// <param name="PlayerRound">
	/// Manages the flow of a player's round, including handling multiple hands.
	/// </param>
	public record PlayerServices<TImage, TValue>(IPlayerAction<TImage, TValue> PlayerAction, IPlayerRound<TImage, TValue> PlayerRound);
}
