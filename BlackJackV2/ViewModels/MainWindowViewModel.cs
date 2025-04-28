// Project: BlackJackV2
// file: BlackJackV2/ViewModels/MainWindowViewModel.cs

/// <summary>
///		This class is the view model for the main window of the application.
///		And it contains the view models for the button and table.
///		
///		GameLogic		gameLogic		: The game logic for the game.
///		StatsViewModel	StatsViewModel	: View model for displaying the current stats (will probubly be removed).
///		TableViewModel	TableViewModel	: View model for displaying the table (With Dealer and Player Hands).
///		ButtonViewModel ButtonViewModel : View model for displaying the buttons (Hit, Stand, etc).
///		
/// </summary>

using System.Collections.Generic;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using BlackJackV2.Models.GameLogic;
using BlackJackV2.Models.GameLogic.PlayerServices;

namespace BlackJackV2.ViewModels
{
	public class MainWindowViewModel : ViewModelBase
    {

		private GameLogic<Bitmap, string> _gameLogic;
		
		public StatsViewModel StatsViewModel { get; }
		public TableViewModel TableViewModel { get; }
		public ButtonViewModel ButtonViewModel { get; }
		
		public MainWindowViewModel(	GameLogic<Bitmap, string> gameLogic, 
									IPlayerServices<Bitmap, string> playerServices,
									TableViewModel tableViewModel, 
									StatsViewModel statsViewModel)
		{
			_gameLogic = gameLogic;
			StatsViewModel = statsViewModel;
			TableViewModel = tableViewModel;

			//StatsViewModel = ViewModelCreator.CreateStatsViewModel(gameCoordinator);
			ButtonViewModel = ViewModelCreator.CreateButtonViewModel(playerServices.PlayerRound);

			// Schedule game start AFTER UI loads
			Task.Run(async () =>
			{
				await Task.Delay(100); // short delay to ensure window shows
				playerServices.OnPlayerChangedReceived(new List<string> { "Player1", "Player2" });
				gameLogic.RunGameLoop();
			});
		}
	}
}
