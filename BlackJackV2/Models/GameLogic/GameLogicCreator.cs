// Project: BlackJackV2
// file: BlackJackV2/Models/GameLogic/GameLogicCreator.cs

/// <summary>
///		Creates objects related to GameLogic
///		
///		static GameLogic		CreateGameLogic()									: Return new GameLogic object.
///		static RoundEvaluator	CreateRoundEvaluator()								: Return new RoundEvaluator object.
///		static PlayerAction		CreatePlayerAction(Subject<SplitSuccessfulEvent>)	: Return new PlayerAction object.
///		static PlayerRound		CreatePlayerRound(PlayerAction, 
///													Subject<SplitSuccessfulEvent>)	: Return new PlayerRound object.
///		static DealerLogic		CreateDealerLogic()									: Return new DealerLogic object.
///		
/// </summary>

using System.Reactive.Subjects;
using BlackJackV2.Services.Events;

namespace BlackJackV2.Models.GameLogic
{
	public class GameLogicCreator
	{
		//public static GameLogic CreateGameLogic()
		//{
		//	return new GameLogic();
		//}

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
