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
///		readonly CompositeDisposable			_disposables			: Used to clean up resources	
/// 
///		void	UpdatePlayerViewModels(Dictionary<string, IPlayer>)		: Update the player view models when the player changed event is received
///		void	Dispose()												: Cleans up resources
///		
/// </summary>

using BlackJackV2.Models.Player;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Disposables;
using Avalonia.Media.Imaging;
using BlackJackV2.Services.Events;
using System.Reactive.Subjects;
using BlackJackV2.Models.GameLogic.Dealer_Services;
using BlackJackV2.Models.GameLogic.PlayerServices;

namespace BlackJackV2.ViewModels
{
	public class TableViewModel : ReactiveObject
	{
		private readonly IPlayerServices<Bitmap, string> _playerServices;
		private readonly Subject<SplitSuccessfulEvent> _splitEvent;
		private readonly Subject<BetUpdateEvent> _betUpdateEvent;

		// View models for the dealers
		public CardHandViewModel DealerCardHandViewModel { get; }
	
		// A collection of player view models
		public ObservableCollection<PlayerViewModel> playerViewModels { get; private set; }

		private readonly CompositeDisposable _disposables = new CompositeDisposable();
		public TableViewModel(	IPlayerServices<Bitmap, string> playerServices, 
								IDealerServices<Bitmap, string> dealerServices, 
								Subject<SplitSuccessfulEvent> splitEvent, 
								Subject<BetUpdateEvent> betUpdateEvent)

		{
			_playerServices = playerServices;
			_splitEvent = splitEvent;
			_betUpdateEvent = betUpdateEvent;

			DealerCardHandViewModel = ViewModelCreator.CreateHandCardViewModel(dealerServices.DealerCardHand.PrimaryCardHand);

			playerViewModels = new ObservableCollection<PlayerViewModel>();

			playerServices.PlayerChangedEvent
				.Subscribe(playerEvent =>{
					// Update the player view models when the player event is received
					UpdatePlayerViewModels(playerEvent);
			}).DisposeWith(_disposables);
	
		}

		// Update the player view models when the player changed event is received
		public void UpdatePlayerViewModels(Dictionary<string, IPlayer<Bitmap, string>> playerEvent)
		{
			// Replace the old player view models with the new ones
			playerViewModels = new ObservableCollection<PlayerViewModel>();

			foreach (var player in playerEvent)
			{
				PlayerViewModel playerViewModel = ViewModelCreator.CreatePlayerViewModel(player.Value, _splitEvent, _betUpdateEvent);
				playerViewModels.Add(playerViewModel);
			}
		}
		
		public void Dispose() => _disposables.Dispose();
	}
}


