// Project: BlackJackV2
// file: BlackJackV2/Services/Events/SplitSuccessfulEvent.cs

/// <summary>
///		
///		This class is responsible for handling the split successful events in the game.
///		It contains the player name and is used to notify when a split has been successful.
///		
///		string PlayerName	: The name of the player who successfully split their hand.
///		
/// </summary> 

namespace BlackJackV2.Services.Events
{
	/**
	 * This class is responsible for handling the split successful events in the game.
	 * It contains the player name and is used to notify when a split has been successful.
	 **/
	public class SplitSuccessfulEvent
	{
		public string PlayerName { get; }

		public SplitSuccessfulEvent(string playerName) 
		{
			PlayerName = playerName;
		}
	}
}
