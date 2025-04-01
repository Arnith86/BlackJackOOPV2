using BlackJackV2.Models;
using ReactiveUI;
using System.Collections.ObjectModel;

namespace BlackJackV2.ViewModels
{
	public class TableViewModel : ReactiveObject
	{
		// These properties could be other view models for sub-views, for instance.
		public CardHandViewModel DealerCardHandViewModel { get; }
		public CardHandViewModel PlayerCardHandViewModel { get; }
		public CardHandViewModel PlayerSplitCardHandViewModel { get; }

		// Optionally, if you have collections, expose them too.
		public ObservableCollection<CardHandViewModel> PlayerCardViewModels { get; }

		public TableViewModel(GameLogic gameLogic)
		{
			// Create sub-view models (using your ViewModelCreator or directly)
			DealerCardHandViewModel = ViewModelCreator.CreateHandCardViewModel("Dealer Hand", gameLogic.DealerCardHand.PrimaryCardHand.Hand);
			PlayerCardHandViewModel = ViewModelCreator.CreateHandCardViewModel("Player Hand", gameLogic.PlayerCardHand.PrimaryCardHand.Hand);
			PlayerSplitCardHandViewModel = ViewModelCreator.CreateHandCardViewModel("Player Split Hand", gameLogic.PlayerCardHand.SplitCardHand.Hand);

			// You can create a collection of player hand view models if needed
			PlayerCardViewModels = new ObservableCollection<CardHandViewModel>
			{
				PlayerCardHandViewModel,
				PlayerSplitCardHandViewModel
			};
		}
		public void OnPlayerSplit(string splitValue)
		{
		}
	}
}


