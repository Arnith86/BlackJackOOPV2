// Project: BlackJackV2
// file: BlackJackV2/Services/Events/BetUpdateEvent.cs

using BlackJackV2.Shared.Constants;

namespace BlackJackV2.Services.Events
{
	/// <summary>
	/// Represents an event that notifies when a player's bet has been updated.
	/// Contains the player's name and the specific hand affected.
	/// </summary>
	/// <param name="PlayerName">The name of the player whose bet has been updated.</param>
	/// <param name="HandOwner">The owner of the hand (primary or split) that has been updated.</param>
	public record BetUpdateEvent(string PlayerName, HandOwners.HandOwner HandOwner);
}
