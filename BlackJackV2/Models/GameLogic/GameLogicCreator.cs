using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackV2.Models.GameLogic
{
	public class GameLogicCreator
	{
		public static GameLogic CreateGameLogic()
		{
			return new GameLogic();
		}
	}
}
