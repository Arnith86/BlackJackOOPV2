// Project: BlackJackV2
// file: BlackJackV2/Services/Events/SplitSuccessfulEvent.cs

namespace BlackJackV2.Services.Events
{
	/// <summary>
	/// Represent an event that notifies when a player has successfully split their hand. 
	/// Contains the specified player's name.
	/// </summary>
	/// <param name="PlayerName"></param>
	public record SplitSuccessfulEvent(string PlayerName);
}
