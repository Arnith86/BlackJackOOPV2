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

		public static StatsViewModel CreateStatsViewModel(GameLogic gameLogic)
		{
			return new StatsViewModel(gameLogic);
		}

		public static ButtonViewModel CreateButtonViewModel(PlayerRound playerRound)
		{
			return new ButtonViewModel(playerRound);
		}

		public static TableViewModel CreateTableViewModel(GameLogic gameLogic)
		{
			return new TableViewModel(gameLogic);
		}

		public static PlayerViewModel CreatePlayerViewModel(IPlayer player, GameLogic gameLogic)
		{
			return new PlayerViewModel(player, gameLogic);
		}
	}
}
