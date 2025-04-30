// Project: BlackJackV2
// file: BlackJackV2/Services/Events/PlayerActionEvent.cs

using BlackJackV2.Shared.Constants;

namespace BlackJackV2.Services.Events
{
	/// <summary>
	/// Represents an event containing information about a player's action during a game round,
	/// including the initiating hand owner, the target hand, and the specific action taken.
	/// </summary>
	public record PlayerActionEvent(
		string PlayerName, 
		HandOwners.HandOwner PrimaryOrSplit, 
		BlackJackActions.PlayerActions PlayerAction
	);
	
}
