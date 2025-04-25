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
using BlackJackV2.Models.GameLogic.Dealer_Services;
using BlackJackV2.Models.GameLogic.PlayerServices;
using BlackJackV2.Shared.Constants;

namespace BlackJackV2.Models.GameLogic
{
	public class GameLogicCreator<TImage, TValue>
	{

		public static RoundEvaluator<TImage, TValue> CreateRoundEvaluator() 
		{ 
			return new RoundEvaluator<TImage, TValue>(); 
		}

		public static PlayerAction<TImage, TValue> CreatePlayerAction(Subject<SplitSuccessfulEvent> splitSuccessfulEvent ) 
		{ 
			return new PlayerAction<TImage, TValue>(splitSuccessfulEvent); 
		}

		public static PlayerRound<TImage, TValue> CreatePlayerRound(PlayerAction<TImage, TValue> playerAction, Subject<BlackJackActions.PlayerActions> playerActionSubject, Subject<SplitSuccessfulEvent> splitSuccessfulEvent)
		{
			return new PlayerRound<TImage, TValue>(playerAction, playerActionSubject, splitSuccessfulEvent);
		}

		public static DealerServices<TImage, TValue> CreateDealerLogic()
		{
			return new DealerServices<TImage, TValue>();
		}
	}
}
