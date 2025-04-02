using BlackJackV2.Models.GameLogic;
using ReactiveUI;
using System.Collections.ObjectModel;

namespace BlackJackV2.ViewModels
{
	/**
	 * This class is used to represent the table in the view
	 * Here we can have the dealer's hand, player's hand, and player's split hand
	 **/

	public class TableViewModel : ReactiveObject
	{
		// Create the view models for the dealer and player hands
		public CardHandViewModel DealerCardHandViewModel { get; }
		public CardHandViewModel PlayerCardHandViewModel { get; }
		public CardHandViewModel PlayerSplitCardHandViewModel { get; }
		public ObservableCollection<CardHandViewModel> PlayerCardViewModels { get; private set; }

		public TableViewModel(GameLogic gameLogic)
		{
			DealerCardHandViewModel = ViewModelCreator.CreateHandCardViewModel("Dealer Hand", gameLogic.DealerCardHand.PrimaryCardHand.Hand);
			PlayerCardHandViewModel = ViewModelCreator.CreateHandCardViewModel("Player Hand", gameLogic.PlayerCardHand.PrimaryCardHand.Hand);
			PlayerSplitCardHandViewModel = ViewModelCreator.CreateHandCardViewModel("Player Split Hand", gameLogic.PlayerCardHand.SplitCardHand.Hand);

			// Add the player primary hand to the player card view models
			PlayerCardViewModels = new ObservableCollection<CardHandViewModel>
			{
				PlayerCardHandViewModel
			};
		}

		// Add the player split hand to the to PlayerCardViewModels (adds another view in the UI)
		public void OnPlayerSplit(string splitValue)
		{
			PlayerCardViewModels.Add(PlayerSplitCardHandViewModel);
		}

		// Remove the player split hand from the PlayerCardViewModels (removes the view from the UI)
		public void OnPlayerSplitEnd()
		{
			PlayerCardViewModels.Remove(PlayerSplitCardHandViewModel);
		}
	}
}


