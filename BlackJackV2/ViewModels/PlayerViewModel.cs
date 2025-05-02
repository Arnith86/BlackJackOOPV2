// Project: BlackJackV2
// file: BlackJackV2/ViewModels/PlayerViewModel.cs

using Avalonia.Media.Imaging;
using BlackJackV2.Models.GameLogic.GameRuleServices;
using BlackJackV2.Models.GameLogic.PlayerServices;
using BlackJackV2.Models.Player;
using BlackJackV2.Services.Events;
using BlackJackV2.Shared.Constants;
using BlackJackV2.ViewModels.Interfaces;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace BlackJackV2.ViewModels
{
	public class PlayerViewModel : ReactiveObject, IPlayerViewModel
	{
		private int _funds;
		private IPlayer<Bitmap, string> _player;
		private readonly CompositeDisposable _disposables = new CompositeDisposable();

		/// <summary>
		/// Gets the view model representing the player's primary card hand. Initialized during construction.
		/// </summary>
		public ICardHandViewModel PlayerCardHandViewModel { get; }

		/// <summary>
		/// View model for the player's split hand. Created during construction but
		/// only shown when a split occurs.
		/// </summary>
		public ICardHandViewModel PlayerSplitCardHandViewModel { get; }

		/// <summary>
		/// Holds the set of hand view models displayed in the UI. Always includes
		/// the primary hand; the split hand is added/removed dynamically.
		/// </summary>
		public ObservableCollection<ICardHandViewModel> PlayerCardViewModels { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="PlayerViewModel"/> class,
		/// wiring up player data, reactive UI updates, and event handlers related to splits and bets.
		/// </summary>
		/// <param name="player">The <see cref="IPlayer"/> associated with this view model.</param>
		/// <param name="splitSuccessfulEvent">The event that is triggered when a player successfully split their hand.</param>
		/// <param name="betUpdateEvent">The event that is triggered when a player updates their bet.</param>
		/// <param name="betRequestEvent">Event triggered when a bet is requested from the player.</param>
		/// <param name="viewModelCreator">A wrapper class containing factories for creating view models.</param>
		/// <param name="playerServices">Handles player-specific services and actions.</param>
		/// <param name="gameRuleServices">Handles game rules and logic.</param>
		/// <remarks>
		/// Related files <see cref="BlackJackV2.Factories.PlayerViewModelFactory"/>
		/// </remarks>
		public PlayerViewModel(	IPlayer<Bitmap, string> player, 
								Subject<SplitSuccessfulEvent> splitSuccessfulEvent, 
								Subject<BetUpdateEvent> betUpdateEvent,
								Subject<BetRequestEvent<Bitmap, string>> betRequestEvent,
								IViewModelCreator viewModelCreator,
								IPlayerServices<Bitmap, string> playerServices,
								GameRuleServices<Bitmap, string> gameRuleServices) 
		{
			Player = player;
		
			PlayerCardHandViewModel = viewModelCreator.BuildCardHandViewModel(
				HandOwners.HandOwner.Primary,player, playerServices, gameRuleServices);
			
			PlayerSplitCardHandViewModel = viewModelCreator.BuildCardHandViewModel(
				HandOwners.HandOwner.Split, player, playerServices, gameRuleServices);

			// Add the player primary hand to the player card view models
			PlayerCardViewModels = new ObservableCollection<ICardHandViewModel>
			{
				PlayerCardHandViewModel
			};

			// Listen for the player funds event 
			this.WhenAnyValue(x => x.Player)
				.Where(player => player != null)
				.SelectMany(player => player.WhenAnyValue(p => p.Funds))
				.Subscribe(funds => Funds = funds)
				.DisposeWith(_disposables);

			betUpdateEvent
			.Subscribe(betEvent => 
			{
				// Update the player bet when the bet is updated
				SyncPlayerBet(betEvent.PlayerName);
			
			}).DisposeWith(_disposables);

			// Listen for the player split event
			splitSuccessfulEvent
			.Subscribe(splitEvent =>
			{
				// If the player split was successful, add the split hand to the player card view models
				// and update the bet values
				if (splitEvent.PlayerName == Player.Name)
				{
					OnPlayerSplit();
					SyncPlayerBet(splitEvent.PlayerName);
				}
			}).DisposeWith(_disposables);
		}

		/// <summary>
		/// Represents the player's current funds in the UI. 
		/// Automatically updated in response to model changes via reactive bindings.
		/// </summary>
		public int Funds 
		{
			get => _funds;
			set => this.RaiseAndSetIfChanged(ref _funds, value);
		}

		/// <summary>
		/// Backing model for this view model. Used to observe changes like funds or hand state.
		/// </summary>
		public IPlayer<Bitmap, string> Player
		{
			get => _player;
			private set => _player = value;
		}


		/// <summary>
		/// Synchronizes the view model's bet values with the player's model,
		/// only if the specified player name matches this instance.
		/// </summary>
		public void SyncPlayerBet(string playerName)
		{
			PlayerCardHandViewModel.Bet =  Player.Hands.GetBetFromHand(HandOwners.HandOwner.Primary);
			PlayerSplitCardHandViewModel.Bet = Player.Hands.GetBetFromHand(HandOwners.HandOwner.Split);
		}

		/// <summary>
		/// Adds the split hand to the observable collection of hand view models, 
		/// triggering an additional hand display in the UI.
		/// </summary>
		public void OnPlayerSplit()
		{
			PlayerCardViewModels.Add(PlayerSplitCardHandViewModel);
		}

		/// <summary>
		/// Removes the split hand from the observable collection,
		/// collapsing the additional hand view in the UI.
		/// </summary>
		public void OnPlayerSplitEnd()
		{
			PlayerCardViewModels.Remove(PlayerSplitCardHandViewModel);
		}

		/// <summary>
		/// Cleans up reactive subscriptions and disposables tied to this view model.
		/// </summary>
		public void Dispose() => _disposables.Dispose();
	}
}
