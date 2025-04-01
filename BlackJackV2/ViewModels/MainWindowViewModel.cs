using Avalonia.Collections;
using Avalonia.Media.Imaging;
using BlackJackV2.Models;
using BlackJackV2.Models.CardDeck;
using BlackJackV2.Models.CardFactory;
using BlackJackV2.Models.CardHand;
using BlackJackV2.Models.Player;
using ReactiveUI;
using System.Collections.ObjectModel;

namespace BlackJackV2.ViewModels
{
	public class MainWindowViewModel : ViewModelBase
    {
		Bitmap _testImage;
		private GameLogic gameLogic = new GameLogic();
		

		//private CardHandViewModel _playerCardHandViewModel;
		//private CardHandViewModel _playerSplitCardHandViewModel;

		//public CardHandViewModel DealerCardHandViewModel { get; }
		//public CardHandViewModel PlayerCardHandViewModel /*{ get; }*/
		//{  
		//	get => _playerCardHandViewModel;
		//	set => this.RaiseAndSetIfChanged(ref _playerCardHandViewModel, value);
		//}
		
		//public CardHandViewModel PlayerSplitCardHandViewModel /*{ get; }*/
		//{
		//	get => _playerSplitCardHandViewModel;
		//	set => this.RaiseAndSetIfChanged(ref _playerSplitCardHandViewModel, value);
		//}

		//private ObservableCollection<CardHandViewModel> _playerCardViewModels;
		//public ObservableCollection<CardHandViewModel> PlayerCardViewModels
		//{
		//	get => _playerCardViewModels;
		//	set => this.RaiseAndSetIfChanged(ref _playerCardViewModels, value);
		//}

		//private AvaloniaDictionary<string, CardHandViewModel> _playerCardViewModels;
		//public AvaloniaDictionary<string, CardHandViewModel> PlayerCardViewModels
		//{
		//	get => _playerCardViewModels;
		//	set => this.RaiseAndSetIfChanged(ref _playerCardViewModels, value);
		//}
		public ButtonViewModel ButtonViewModel { get; }
		public TableViewModel TableViewModel { get; }

		public MainWindowViewModel()
		{
			//DealerCardHandViewModel = ViewModelCreator.CreateHandCardViewModel("Dealer Hand", gameLogic.DealerCardHand.PrimaryCardHand.Hand);
			//PlayerCardHandViewModel = ViewModelCreator.CreateHandCardViewModel("Player Hand", gameLogic.PlayerCardHand.PrimaryCardHand.Hand);
			//PlayerSplitCardHandViewModel = ViewModelCreator.CreateHandCardViewModel("Player Split Hand", gameLogic.PlayerCardHand.SplitCardHand.Hand);
			////PlayerCardViewModels = new ObservableCollection<CardHandViewModel> { PlayerCardHandViewModel, PlayerSplitCardHandViewModel };
			//PlayerCardViewModels = new AvaloniaDictionary<string, CardHandViewModel> { {PlayerCardHandViewModel.Id, PlayerCardHandViewModel }, {PlayerSplitCardHandViewModel.Id, PlayerSplitCardHandViewModel } };
			TableViewModel = ViewModelCreator.CreateTableViewModel(gameLogic);
			ButtonViewModel = ViewModelCreator.CreateButtonViewModel(gameLogic);

		}
		public void OnPlayerSplit(string splitValue)
		{
			

		}



		public Bitmap TestImage
		{
			get => _testImage;
			set => this.RaiseAndSetIfChanged(ref _testImage, value);
		}
	}
}
