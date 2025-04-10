﻿using BlackJackV2.Constants;
using BlackJackV2.Models.GameLogic;
using BlackJackV2.Services.Messaging;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reactive.Linq;
using System;

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
			DealerCardHandViewModel = ViewModelCreator.CreateHandCardViewModel(HandOwners.HandOwner.Dealer, 
																				gameLogic.DealerCardHand.PrimaryCardHand, 
																				gameLogic.DealerCardHand.PrimaryCardHand.HandValue.ToString());
			PlayerCardHandViewModel = ViewModelCreator.CreateHandCardViewModel(HandOwners.HandOwner.Primary, 
																				gameLogic.PlayerCardHand.PrimaryCardHand,
																				gameLogic.PlayerCardHand.PrimaryCardHand.HandValue.ToString());
			PlayerSplitCardHandViewModel = ViewModelCreator.CreateHandCardViewModel(HandOwners.HandOwner.Split, 
																				gameLogic.PlayerCardHand.SplitCardHand, 
																				gameLogic.PlayerCardHand.SplitCardHand.HandValue.ToString());

			// Add the player primary hand to the player card view models
			PlayerCardViewModels = new ObservableCollection<CardHandViewModel>
			{
				PlayerCardHandViewModel
			};


			// Listen for the player split event
			MessageBus.Current.Listen<SplitSuccessfulMessage>().Subscribe(splitPerformed =>
			{
				// If the player split was successful, add the split hand to the player card view models
				if (splitPerformed.IsSplitSuccessful) OnPlayerSplit();
			});
		}

		// Add the player split hand to the to PlayerCardViewModels (adds another view in the UI)
		public void OnPlayerSplit()
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


