// Project: BlackJackV2
// file: BlackJackV2/ViewModels/TableViewModel.cs

/// <summary>
///
///		This class is used to represent the table in the view
///		Here we have the dealer's hand and a collection of player hands
///		
///		GameLogic								_gameLogic				: The game logic for the game 
///		CardHandViewModel						DealerCardHandViewModel : The dealer's hand view model 
///		ObservableCollection<PlayerViewModel>	playerViewModels		: A collection of player view models
///		
///		void	UpdatePlayerViewModels(Dictionary<string, IPlayer>)		: Update the player view models when the player changed event is received 
/// </summary>

using BlackJackV2.Models.GameLogic;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Reactive.Linq;
using System;
using BlackJackV2.Models.Player;

namespace BlackJackV2.ViewModels
{
	public class TableViewModel : ReactiveObject
	{
		GameLogic _gameLogic; 

		// View models for the dealers
		public CardHandViewModel DealerCardHandViewModel { get; }
	

		// A collection of player view models
		public ObservableCollection<PlayerViewModel> playerViewModels { get; private set; }

		public TableViewModel(GameLogic gameLogic)
		{
			_gameLogic = gameLogic;

			DealerCardHandViewModel = ViewModelCreator.CreateHandCardViewModel(gameLogic.DealerCardHand.PrimaryCardHand);

			playerViewModels = new ObservableCollection<PlayerViewModel>();

			gameLogic.PlayerChangedEvent.Subscribe(playerEvent =>
			{
				// Update the player view models when the player event is received
				UpdatePlayerViewModels(playerEvent);
			});
	
		}

		// Update the player view models when the player changed event is received
		public void UpdatePlayerViewModels(Dictionary<string, IPlayer> playerEvent)
		{
			// Replace the old player view models with the new ones
			playerViewModels = new ObservableCollection<PlayerViewModel>();

			foreach (var player in playerEvent)
			{
				PlayerViewModel playerViewModel = ViewModelCreator.CreatePlayerViewModel(player.Value, _gameLogic);
				playerViewModels.Add(playerViewModel);
			}
		}
	}
}


