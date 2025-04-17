using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
