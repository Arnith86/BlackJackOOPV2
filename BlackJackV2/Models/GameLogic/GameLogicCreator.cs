using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using BlackJackV2.Models.CardDeck;
using BlackJackV2.Models.Player;
using BlackJackV2.Services.Events;

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

		public static PlayerAction CreatePlayerAction(Subject<SplitSuccessfulEvent> splitSuccessfulEvent ) 
		{ 
			return new PlayerAction(splitSuccessfulEvent); 
		}

		public static PlayerRound CreatePlayerRound(PlayerAction playerAction, Subject<SplitSuccessfulEvent> splitSuccessfulEvent)
		{
			return new PlayerRound(playerAction, splitSuccessfulEvent);
		}

		public static DealerLogic CreateDealerLogic()
		{
			return new DealerLogic();
		}
	}
}
