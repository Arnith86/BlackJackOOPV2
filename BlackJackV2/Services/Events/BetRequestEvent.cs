// Project: BlackJackV2
// file: BlackJackV2/Services/Events/BetRequestEvent.cs

using BlackJackV2.Models.Player;

namespace BlackJackV2.Services.Events
{
	/// <summary>
	/// Represents a request or update related to a player's betting status in a game round.
	/// </summary>
	/// <param name="PlayerName">The name of the player whose bet status is being reported.</param>
	/// <param name="IsBetRegistered">Indicates whether the player's bet has been successfully registered or still waiting.</param>
	public record BetRequestEvent<TImage, TValue>(IPlayer<TImage, TValue> Player, bool IsBetRegistered);
}
