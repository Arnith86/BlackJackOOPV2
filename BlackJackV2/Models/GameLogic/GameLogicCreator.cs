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
using BlackJackV2.Constants;
using BlackJackV2.Services.Events;
using BlackJackV2.Models.GameLogic.Dealer_Services;
using BlackJackV2.Models.GameLogic.PlayerServices;

namespace BlackJackV2.Models.GameLogic
{
	public class GameLogicCreator
	{

		public static RoundEvaluator CreateRoundEvaluator() 
		{ 
			return new RoundEvaluator(); 
		}

		public static PlayerAction CreatePlayerAction(Subject<SplitSuccessfulEvent> splitSuccessfulEvent ) 
		{ 
			return new PlayerAction(splitSuccessfulEvent); 
		}

		public static PlayerRound CreatePlayerRound(PlayerAction playerAction, Subject<BlackJackActions.PlayerActions> playerActionSubject, Subject<SplitSuccessfulEvent> splitSuccessfulEvent)
		{
			return new PlayerRound(playerAction, playerActionSubject, splitSuccessfulEvent);
		}

		public static DealerServices CreateDealerLogic()
		{
			return new DealerServices();
		}
	}
}
