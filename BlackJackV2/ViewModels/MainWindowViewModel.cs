using Avalonia.Collections;
using Avalonia.Media.Imaging;
using BlackJackV2.Models.CardDeck;
using BlackJackV2.Models.CardFactory;
using BlackJackV2.Models.CardHand;
using BlackJackV2.Models.GameLogic;
using BlackJackV2.Models.Player;
using ReactiveUI;
using System.Collections.ObjectModel;

namespace BlackJackV2.ViewModels
{
	/**
	 * This class is the view model for the main window of the application.
	 * And it contains the view models for the button and table.
	 **/
	public class MainWindowViewModel : ViewModelBase
    {
		Bitmap _testImage;
		private PlayerRound _playerRound;
		private GameLogic gameLogic = GameLogicCreator.CreateGameLogic();
		
		public StatsViewModel StatsViewModel { get; }
		public TableViewModel TableViewModel { get; }
		public ButtonViewModel ButtonViewModel { get; }
		
		public MainWindowViewModel()
		{
			StatsViewModel = ViewModelCreator.CreateStatsViewModel(gameLogic);
			TableViewModel = ViewModelCreator.CreateTableViewModel(gameLogic);
			ButtonViewModel = ViewModelCreator.CreateButtonViewModel(gameLogic.playerRound);

			gameLogic.StartNewRound(); // Move or remove when not needed
		}
	}
}
