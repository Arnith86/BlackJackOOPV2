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

namespace BlackJackV2.ViewModels
{
	public class MainWindowViewModel : ViewModelBase
    {

		private GameLogic _gameLogic;
		
		public StatsViewModel StatsViewModel { get; }
		public TableViewModel TableViewModel { get; }
		public ButtonViewModel ButtonViewModel { get; }
		
		public MainWindowViewModel(GameLogic gameLogic, IGameCoordinator<Bitmap, string> gameCoordinator, IPlayerRound<Bitmap, string> playerRound)
		{
			_gameLogic = gameLogic;

			StatsViewModel = ViewModelCreator.CreateStatsViewModel(gameCoordinator);
			TableViewModel = ViewModelCreator.CreateTableViewModel(gameCoordinator);
			ButtonViewModel = ViewModelCreator.CreateButtonViewModel(playerRound);

			// Schedule game start AFTER UI loads
			Task.Run(async () =>
			{
				await Task.Delay(100); // short delay to ensure window shows
				gameCoordinator.OnPlayerChangedReceived(new List<string> { "Player1", "Player2" });
				gameLogic.RunGameLoop();
			});
		}
	}
}
