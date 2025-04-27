// Project: BlackJackV2
// file: BlackJackV2/ViewModels/ViewModelCreator.cs

/// <summary>
///		This class is used to create the view models for the game		
/// </summary>

using Avalonia.Media.Imaging;
using BlackJackV2.Models.CardHand;
using BlackJackV2.Models.GameLogic;
using BlackJackV2.Models.Player;
using BlackJackV2.Services.Events;
using System.Reactive.Subjects;

namespace BlackJackV2.ViewModels
{
	public class ViewModelCreator
	{
		public static CardHandViewModel CreateHandCardViewModel(IBlackJackCardHand<Bitmap, string> cardHand)
		{
			return new CardHandViewModel(cardHand);
		}

		//public static StatsViewModel CreateStatsViewModel(IGameCoordinator<Bitmap, string> gameCoordinator)
		//{
		//	return new StatsViewModel(gameCoordinator);
		//}

		public static ButtonViewModel CreateButtonViewModel(IPlayerRound<Bitmap, string> playerRound)
		{
			return new ButtonViewModel(playerRound);
		}

		//public static TableViewModel CreateTableViewModel(IGameCoordinator<Bitmap, string> gameCoordinator, Subject<SplitSuccessfulEvent> splitEvent, Subject<BetUpdateEvent> betUpdateEvent)
		//{
		//	return new TableViewModel(gameCoordinator, splitEvent, betUpdateEvent);
		//}

		public static PlayerViewModel CreatePlayerViewModel(IPlayer<Bitmap, string> player, Subject<SplitSuccessfulEvent> splitEvent, Subject<BetUpdateEvent> betUpdateEvent)
		{
			return new PlayerViewModel(player, splitEvent, betUpdateEvent);
		}
	}
}
