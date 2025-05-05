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
using Avalonia.Threading;
using BlackJackV2.Models.GameLogic;
using BlackJackV2.Models.GameLogic.PlayerServices;
using BlackJackV2.ViewModels.Interfaces;

namespace BlackJackV2.ViewModels
{
	public class MainWindowViewModel : ViewModelBase
    {

		private GameLogic<Bitmap, string> _gameLogic;
		
		public IPlayerSetupViewModel PlayerSetupViewModel { get; }
		public TableViewModel TableViewModel { get; }
				
		public MainWindowViewModel(	
			GameLogic<Bitmap, string> gameLogic, 
			IPlayerServices<Bitmap, string> playerServices,
			TableViewModel tableViewModel, 
			IPlayerSetupViewModel playerSetupViewModel)
		{
			_gameLogic = gameLogic;
			PlayerSetupViewModel = playerSetupViewModel;
			TableViewModel = tableViewModel;
			
			//Dispatcher.UIThread.Post(async () =>
			//{
			//	await Task.Delay(500); // Ensure UI has time to load
			//	playerServices.OnPlayerChangedReceived(new List<string> { "Player1", "Player2" });
			//	//await gameLogic.RunGameLoop();
			//});
			//Task.Run(async () =>
			//{
			//	await Task.Delay(500); // Wait a bit for UI to finish initializing
			//	playerServices.OnPlayerChangedReceived(new List<string> { "Player1", "Player2" });
			//	await _gameLogic.RunGameLoop(); // ✔️ background thread
			//});
		}


		//// ViewModel
		//public async void OnLoaded()
		//{
		//	await Task.Delay(100); // Allow time for the UI to render
		//	_gameLogic.RunGameLoop();
		//}
	}
}
