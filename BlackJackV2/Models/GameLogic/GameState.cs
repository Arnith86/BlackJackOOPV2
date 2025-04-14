using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackV2.Models.GameLogic
{
	/**
	 * This class is used to represent the game state
	 * It contains the points and bet of the player
	 **/

	public class GameState()
	{
		public int Points { get; set; } = 0;
		public int Bet { get; set; } = 0;

		public bool IsBetRecieved { get; set; }

	}
}
