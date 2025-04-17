// Project: BlackJackV2
// file: BlackJackV2/Services/Events/BetUpdateEvent.cs

/// <summary>
///		This class is responsible for handling the bet update events in the game.
///		It contains the player hands and is used to notify when a bet has been updated.
///		
///		string					PlayerName	: The name of the player whose bet has been updated.
///		HandOwners.HandOwner	HandOwner	: The specific hand of the player whose bet has been updated.
///
/// </summary>

using BlackJackV2.Constants;

namespace BlackJackV2.Services.Events
{
	public class BetUpdateEvent
	{
		public string PlayerName { get; }
		public HandOwners.HandOwner HandOwner { get; }

		public BetUpdateEvent(string playerName, HandOwners.HandOwner handOwner)
		{
			PlayerName = playerName;
			HandOwner = handOwner;
		}
	}
}
