// Project: BlackJackV2
// file: BlackJackV2/Services/Events/SplitSuccessfulEvent.cs

namespace BlackJackV2.Services.Events
{
	/// <summary>
	/// Represent an event that notifies when a player has successfully split their hand. 
	/// Contains the specified player's name.
	/// </summary>
	/// <param name="PlayerName">Player performing the split.</param>
	/// <param name="splitStart">Indicate the state of the split. True = start, False = end.</param>
	public record SplitEvent(string PlayerName, bool splitStart);
}
