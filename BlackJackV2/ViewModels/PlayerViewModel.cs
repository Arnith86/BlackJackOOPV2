using Avalonia.Media.Imaging;
using BlackJackV2.Constants;
using BlackJackV2.Models.GameLogic;
using BlackJackV2.Models.Player;
using BlackJackV2.Services.Messaging;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackV2.ViewModels
{
	public class PlayerViewModel
	{
		public IPlayer Player { get; private set; }
		public CardHandViewModel PlayerCardHandViewModel { get; }
		public CardHandViewModel PlayerSplitCardHandViewModel { get; }
		public ObservableCollection<CardHandViewModel> PlayerCardViewModels { get; private set; }

		public PlayerViewModel(IPlayer player, GameLogic gameLogic) 
		{
			Player = player;

			PlayerCardHandViewModel = ViewModelCreator.CreateHandCardViewModel(player.hands.PrimaryCardHand);
			PlayerSplitCardHandViewModel = ViewModelCreator.CreateHandCardViewModel(player.hands.SplitCardHand);

			// Add the player primary hand to the player card view models
			PlayerCardViewModels = new ObservableCollection<CardHandViewModel>
			{
				PlayerCardHandViewModel
			};

			gameLogic.BetUpdateEvent.Subscribe(betEvent =>
			{
				// Update the player bet when the bet is updated
				SyncPlayerBet(betEvent.PlayerName);
			});

			// Listen for the player split event
			gameLogic.splitSuccessfulEvent.Subscribe(splitEvent =>
			{
				// If the player split was successful, add the split hand to the player card view models
				// and update the bet values
				if (splitEvent.PlayerName == Player.Name)
				{
					OnPlayerSplit();
					SyncPlayerBet(splitEvent.PlayerName);
				}
			});
		}

		// Sync the player bet with the player hands
		public void SyncPlayerBet(string playeName)
		{
			if (playeName == Player.Name) 
			{
				PlayerCardHandViewModel.Bet =  Player.hands.GetBetFromHand(HandOwners.HandOwner.Primary);
				PlayerSplitCardHandViewModel.Bet = Player.hands.GetBetFromHand(HandOwners.HandOwner.Split);
			}
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
