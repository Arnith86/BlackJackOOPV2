// Project: BlackJackV2
// file: BlackJackV2/Models/GameLogic/IGameCoordinator.cs

/// <summary>
///		This is a Facade used for the GameCoordinator 
///		
///		public		Subject<Dictionary<string, IPlayer>>		PlayerChangedEvent		:	Subject to notify if players in game change
///		public		Subject<BetUpdateEvent>						BetUpdateEvent			:	Used to notify when the bet value is updated
///		public		Subject<IPlayer>							BetRequestedEvent		:	Subject to notify when the bet is requested
///		public		Subject<SplitSuccessfulEvent>				splitSuccessfulEvent	:	Subject to notify when the player split is successful
///		public		IObservable<GameState>						GameStateObservable		:	Subject and IObservable to notify when the game state changes
///		
///		public		IBlackJackPlayerHands<Bitmap, string>		DealerCardHand			:	Represents the dealers hands
/// 
/// 	void		RegisterBetForNewRound();							: Initiates player bets retrival 
///		void		StartNewRound()										: Starts a new round of the game, handles player turns and dealer's turn
///		void		EvaluateRound()										: Evaluates the round and determines the winner
///		void		OnBetInputReceived(string playerName, int betInput)	: Called when the player inputs their bet
///		void		OnPlayerChangedReceived(List<string> playerNames)   : Called when the current players are changed
/// </summary>

using Avalonia.Media.Imaging;
using BlackJackV2.Models.Player;
using BlackJackV2.Models.PlayerHands;
using BlackJackV2.Services.Events;
using System;
using System.Collections.Generic;
using System.Reactive.Subjects;
using System.Threading.Tasks;

namespace BlackJackV2.Models.GameLogic
{
	public interface IGameCoordinator<TImage, TValue>
	{
		public Subject<Dictionary<string, IPlayer<TImage, TValue>>> PlayerChangedEvent { get; }
		public Subject<BetUpdateEvent> BetUpdateEvent { get; }
		public Subject<IPlayer<TImage, TValue>> BetRequestedEvent { get; }
		public Subject<SplitSuccessfulEvent> SplitSuccessfulEvent { get; }
		public IObservable<GameState> GameStateObservable { get; }

		public IBlackJackPlayerHands<Bitmap, string> DealerCardHand {  get; }

		public Task RegisterBetForNewRound();
		public Task StartNewRound();
		public void EvaluateRound();
		public void OnBetInputReceived(string playerName, int betInput);
		public void OnPlayerChangedReceived(List<string> playerNames);
	}
}
