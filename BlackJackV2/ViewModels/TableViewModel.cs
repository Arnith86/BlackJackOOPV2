// Project: BlackJackV2
// file: BlackJackV2/ViewModels/TableViewModel.cs

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
using BlackJackV2.ViewModels.Interfaces;
using BlackJackV2.Models.GameLogic.GameRuleServices;


namespace BlackJackV2.ViewModels
{
	/// <summary>
	/// ViewModel responsible for managing the overall Blackjack table, including dealer and player hands.
	/// Subscribes to player change events and rebuilds player view models accordingly.
	/// </summary>
	public class TableViewModel : ReactiveObject
	{
		private readonly IPlayerServices<Bitmap, string> _playerServices;
		private readonly GameRuleServices<Bitmap, string> _gameRuleServices;
		private readonly Subject<SplitSuccessfulEvent> _splitEvent;
		private readonly Subject<BetUpdateEvent> _betUpdateEvent;
		private readonly Subject<BetRequestEvent<Bitmap, string>> _betRequestEvent;
		private readonly IViewModelCreator _viewModelCreator;

		private readonly CompositeDisposable _disposables = new CompositeDisposable();

		/// <summary>
		/// ViewModel representing the dealer's card hand.
		/// </summary>
		public ICardHandViewModel DealerCardHandViewModel { get; }

		/// <summary>
		/// Collection of ViewModels for each player at the table.
		/// </summary>
		public ObservableCollection<IPlayerViewModel> playerViewModels { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="TableViewModel"/> class.
		/// Handles initialization of dealer and player view models, and subscribes to player update events.
		/// </summary>
		/// <param name="playerServices">Service providing access to player-related data and events.</param>
		/// <param name="dealerServices">Service providing access to the dealer's hand.</param>
		/// <param name="gameRuleServices">Service containing game rules logic.</param>
		/// <param name="splitEvent">Event triggered when a split is successful.</param>
		/// <param name="betUpdateEvent">Event triggered when a bet is updated.</param>
		/// <param name="betRequestEvent">Event used to request player bets.</param>
		/// <param name="viewModelCreator">A wrapper class containing factories for creating view models.</param>
		public TableViewModel(	IPlayerServices<Bitmap, string> playerServices, 
								IDealerServices<Bitmap, string> dealerServices,
								GameRuleServices<Bitmap, string> gameRuleServices,
								Subject<SplitSuccessfulEvent> splitEvent, 
								Subject<BetUpdateEvent> betUpdateEvent,
								Subject<BetRequestEvent<Bitmap, string>> betRequestEvent,
								IViewModelCreator viewModelCreator)			
		{
			_playerServices = playerServices;
			_gameRuleServices = gameRuleServices;
			_betRequestEvent = betRequestEvent;
			_splitEvent = splitEvent;
			_betUpdateEvent = betUpdateEvent;
			_viewModelCreator = viewModelCreator;
			
			DealerCardHandViewModel = 
				_viewModelCreator.BlackJackCardHandViewModelCreator.CreateDealerCardHandViewModel(
					dealerServices.DealerCardHand.PrimaryCardHand
				);

			playerViewModels = new ObservableCollection<IPlayerViewModel>();

			playerServices.PlayerChangedEvent
				.Subscribe(playerEvent => {
					// Update the player view models when the player event is received
					UpdatePlayerViewModels(playerEvent);
			}).DisposeWith(_disposables);
	
		}


		/// <summary>
		/// Updates the collection of player view models based on the provided player data.
		/// </summary>
		/// <param name="playerEvent">A dictionary of players keyed by name.</param>
		public void UpdatePlayerViewModels(Dictionary<string, IPlayer<Bitmap, string>> playerEvent)
		{
			// Replace the old player view models with the new ones
			//playerViewModels = new ObservableCollection<IPlayerViewModel>();
			playerViewModels.Clear();

			foreach (var player in playerEvent)
			{
				IPlayerViewModel playerViewModel = BuildPlayerViewModel(player.Value);
				playerViewModels.Add(playerViewModel);
			}
		}

		/// <summary>
		/// Constructs a player view model using the player factory and related dependencies.
		/// </summary>
		/// <param name="player">The player for whom to create the view model.</param>
		/// <returns>A fully constructed <see cref="IPlayerViewModel"/> instance.</returns>
		private IPlayerViewModel BuildPlayerViewModel(IPlayer<Bitmap, string> player)
		{
			return _viewModelCreator.BlackJackPlayerViewModelCreator.CreatePlayerViewModel(
				player,
				_splitEvent,
				_betUpdateEvent,
				_betRequestEvent,
				_viewModelCreator,
				_playerServices,
				_gameRuleServices
			);
		}

		/// <summary>
		/// Disposes subscriptions and resources used by the view model.
		/// </summary>
		public void Dispose() => _disposables.Dispose();
	}
}


