// Project: BlackJackV2
// file: BlackJackV2/ViewModels/ButtonViewModel.cs

using Avalonia.Media.Imaging;
using BlackJackV2.Models.GameLogic.PlayerServices;
using BlackJackV2.ViewModels.Interfaces;
using BlackJackV2.Shared.Constants;
using BlackJackV2.Services.Events;
using ReactiveUI;
using System;
using System.Reactive;
using System.Reactive.Subjects;
using BlackJackV2.Models.Player;


namespace BlackJackV2.ViewModels
{
	/// <summary>
	/// ViewModel that provides commands for player actions in a Blackjack round.
	/// </summary>
	/// /// <remarks>
	/// Related files <see cref="BlackJackV2.Factories.ButtonViewModelFactory"/>
	/// </remarks>
	public class ButtonViewModel : ReactiveObject ,IButtonViewModel
	{
		private IPlayerRound<Bitmap, string> _playerRound;
		private bool _handIsActive;

		/// <summary>
		/// Command for executing the "Hit" action.
		/// </summary>
		public ReactiveCommand<Unit, Unit> HitCommand { get; }

		/// <summary>
		/// Command for executing the "Fold" action.
		/// </summary>
		public ReactiveCommand<Unit, Unit> FoldCommand { get; }

		/// <summary>
		/// Command for executing the "Double Down" action.
		/// </summary>
		public ReactiveCommand<Unit, Unit> DoubleDownCommand { get; }
		
		/// <summary>
		/// Command for executing the "Split" action.
		/// </summary>
		public ReactiveCommand<Unit, Unit> SplitCommand { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="ButtonViewModel"/> class,
		/// wiring up commands to the provided <see cref="IPlayerRound{TImage, TValue}"/>.
		/// </summary>
		/// <param name="playerRound">The current round context that handles player actions.</param>
		public ButtonViewModel(
			IPlayer<Bitmap, string> player,
			HandOwners.HandOwner primaryOrSplit, 
			IPlayerRound<Bitmap, string> playerRound)
		{
			_playerRound = playerRound;
						
			HitCommand = ReactiveCommand.Create(() =>
				_playerRound.PlayerActionSubject.OnNext(new PlayerActionEvent(player.Name, primaryOrSplit, BlackJackActions.PlayerActions.Hit)));

			FoldCommand = ReactiveCommand.Create(() =>
				_playerRound.PlayerActionSubject.OnNext(new PlayerActionEvent(player.Name, primaryOrSplit, BlackJackActions.PlayerActions.Fold)));

			DoubleDownCommand = ReactiveCommand.Create(() =>
				_playerRound.PlayerActionSubject.OnNext(new PlayerActionEvent(player.Name, primaryOrSplit, BlackJackActions.PlayerActions.DoubleDown)));

			SplitCommand = ReactiveCommand.Create(() =>
				_playerRound.PlayerActionSubject.OnNext(new PlayerActionEvent(player.Name, primaryOrSplit, BlackJackActions.PlayerActions.Split)));
		}

		/// <inheritdoc/>
		public bool HandIsActive 
		{
			get => _handIsActive;
			set => this.RaiseAndSetIfChanged(ref _handIsActive, value);
		}
	}
}
