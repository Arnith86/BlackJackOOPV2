// Project: BlackJackV2
// file: BlackJackV2/ViewModels/ViewModelCreator.cs

/// <summary>
///		This class is used to create the view models for the game		
/// </summary>

using Avalonia.Media.Imaging;
using BlackJackV2.Models.CardHand;
using BlackJackV2.Models.GameLogic;
using BlackJackV2.Models.Player;

namespace BlackJackV2.ViewModels
{
	public class ViewModelCreator
	{
		public static CardHandViewModel CreateHandCardViewModel(IBlackJackCardHand<Bitmap, string> cardHand)
		{
			return new CardHandViewModel(cardHand);
		}

		public static StatsViewModel CreateStatsViewModel(IGameCoordinator gameCoordinator)
		{
			return new StatsViewModel(gameCoordinator);
		}

		public static ButtonViewModel CreateButtonViewModel(IPlayerRound playerRound)
		{
			return new ButtonViewModel(playerRound);
		}

		public static TableViewModel CreateTableViewModel(IGameCoordinator gameCoordinator)
		{
			return new TableViewModel(gameCoordinator);
		}

		public static PlayerViewModel CreatePlayerViewModel(IPlayer player, IGameCoordinator gameCoordinator)
		{
			return new PlayerViewModel(player, gameCoordinator);
		}
	}
}
