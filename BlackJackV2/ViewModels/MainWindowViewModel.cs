// Project: BlackJackV2
// file: BlackJackV2/ViewModels/MainWindowViewModel.cs

using Avalonia.Media.Imaging;
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
		}
	}
}
