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
		//public CardHandViewModel PlayerCardHandViewModel { get; }
		//public CardHandViewModel PlayerSplitCardHandViewModel { get; }
		//public ObservableCollection<CardHandViewModel> PlayerCardViewModels { get; private set; }


		// A collection of player view models
		public ObservableCollection<PlayerViewModel> playerViewModels { get; private set; }

		public TableViewModel(GameLogic gameLogic)
		{
			_gameLogic = gameLogic;

			DealerCardHandViewModel = ViewModelCreator.CreateHandCardViewModel(HandOwners.HandOwner.Dealer, 
																				gameLogic.DealerCardHand.PrimaryCardHand, 
																				gameLogic.DealerCardHand.PrimaryCardHand.HandValue.ToString());

			playerViewModels = new ObservableCollection<PlayerViewModel>();

			gameLogic.PlayerChangedEvent.Subscribe(playerEvent =>
			{
				// Update the player view models when the player event is received
				UpdatePlayerViewModels(playerEvent);
			});
			//PlayerCardHandViewModel = ViewModelCreator.CreateHandCardViewModel(HandOwners.HandOwner.Primary, 
			//																	gameLogic.PlayerCardHand.PrimaryCardHand,
			//																	gameLogic.PlayerCardHand.PrimaryCardHand.HandValue.ToString());

			//PlayerSplitCardHandViewModel = ViewModelCreator.CreateHandCardViewModel(HandOwners.HandOwner.Split, 
			//																	gameLogic.PlayerCardHand.SplitCardHand, 
			//																	gameLogic.PlayerCardHand.SplitCardHand.HandValue.ToString());

			//PlayerCardHandViewModel = ViewModelCreator.CreateHandCardViewModel(HandOwners.HandOwner.Primary,
			//																	gameLogic.Players["Player1"].hands.PrimaryCardHand,
			//																	gameLogic.Players["Player1"].hands.PrimaryCardHand.HandValue.ToString());

			//PlayerSplitCardHandViewModel = ViewModelCreator.CreateHandCardViewModel(HandOwners.HandOwner.Split, 
			//																	gameLogic.Players["Player1"].hands.PrimaryCardHand,
			//																	gameLogic.Players["Player1"].hands.PrimaryCardHand.HandValue.ToString());

			//// Add the player primary hand to the player card view models
			//PlayerCardViewModels = new ObservableCollection<CardHandViewModel>
			//{
			//	PlayerCardHandViewModel
			//};

			//gameLogic.BetUpdateEvent.Subscribe( betEvent =>
			//{
			//	// Update the player bet when the bet is updated
			//	SyncPlayerBet(betEvent.PlayerHands);
			//});

			//// Listen for the player split event
			//MessageBus.Current.Listen<SplitSuccessfulMessage>().Subscribe(splitPerformed =>
			//{
			//	// If the player split was successful, add the split hand to the player card view models
			//	// and update the bet values
			//	if (splitPerformed.IsSplitSuccessful)
			//	{
			//		OnPlayerSplit();

			//		SyncPlayerBet(splitPerformed.PlayerHands);
			//	}
			//});
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

		//// Sync the player bet with the player hands
		//public void SyncPlayerBet(IPlayerHands<Bitmap, string> playerHands)
		//{
		//	PlayerCardHandViewModel.Bet = playerHands.GetBetFromHand(HandOwners.HandOwner.Primary);
		//	PlayerSplitCardHandViewModel.Bet = playerHands.GetBetFromHand(HandOwners.HandOwner.Split);
		//}

		//// Add the player split hand to the to PlayerCardViewModels (adds another view in the UI)
		//public void OnPlayerSplit()
		//{
		//	PlayerCardViewModels.Add(PlayerSplitCardHandViewModel);
		//}

		//// Remove the player split hand from the PlayerCardViewModels (removes the view from the UI)
		//public void OnPlayerSplitEnd()
		//{
		//	PlayerCardViewModels.Remove(PlayerSplitCardHandViewModel);
		//}
	}
}


