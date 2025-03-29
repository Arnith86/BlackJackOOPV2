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

		public CardHandViewModel DealerCardViewModel { get; }
		public CardHandViewModel PlayerCardViewModel { get; }
		private ObservableCollection<CardHandViewModel> _playerCardViewModels;
		public ObservableCollection<CardHandViewModel> PlayerCardViewModels { get; }
		public ButtonViewModel ButtonViewModel { get; }

		public MainWindowViewModel()
		{
			DealerCardViewModel = ViewModelCreator.CreateHandCardViewModel("dealer", gameLogic.DealerCardHand.PrimaryCardHand.Hand);
			
			PlayerCardViewModels = new ObservableCollection<CardHandViewModel>();
			PlayerCardViewModels.Add(ViewModelCreator.CreateHandCardViewModel("player1", gameLogic.PlayerCardHand.PrimaryCardHand.Hand));
			this.RaisePropertyChanged(nameof(PlayerCardViewModels));
			PlayerCardViewModels.Add(ViewModelCreator.CreateHandCardViewModel("player2", gameLogic.PlayerCardHand.SplitCardHand.Hand));
			ButtonViewModel = ViewModelCreator.CreateButtonViewModel(gameLogic);

		}
		public void OnPlayerSplit(string splitValue)
		{
			PlayerCardViewModels.Add(ViewModelCreator.CreateHandCardViewModel("player2", gameLogic.PlayerCardHand.SplitCardHand.Hand));
		}
		


		public Bitmap TestImage
		{
			get => _testImage;
			set => this.RaiseAndSetIfChanged(ref _testImage, value);
		}
	}
}
