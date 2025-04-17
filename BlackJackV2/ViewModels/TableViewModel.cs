using BlackJackV2.Constants;
using BlackJackV2.Models.GameLogic;
using BlackJackV2.Services.Messaging;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reactive.Linq;
using System;
using BlackJackV2.Models.Player;
using Avalonia.Media.Imaging;

namespace BlackJackV2.ViewModels
{
	/**
	 * This class is used to represent the table in the view
	 * Here we can have the dealer's hand, player's hand, and player's split hand
	 **/

	public class TableViewModel : ReactiveObject
	{
		GameLogic _gameLogic; 

		// Create the view models for the dealer and player hands
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

		// Update the player view models when the player event is received
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


