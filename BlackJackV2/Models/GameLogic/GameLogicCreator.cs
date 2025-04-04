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

		public static RoundEvaluator CreateRoundEvaluator() 
		{ 
			return new RoundEvaluator(); 
		}

		public static PlayerAction CreatePlayerAction() 
		{ 
			return new PlayerAction(); 
		}

		public static DealerLogic CreateDealerLogic()
		{
			return new DealerLogic();
		}
	}
}
