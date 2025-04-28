// Project: BlackJackV2
// file: BlackJackV2/ViewModels/StatsViewModel.cs

using Avalonia.Media.Imaging;
using BlackJackV2.Models.GameLogic;
using BlackJackV2.Models.GameLogic.GameRuleServices.Interfaces;
using BlackJackV2.Models.Player;
using ReactiveUI;
using System;
using System.Diagnostics;
using System.Reactive;
using System.Reactive.Disposables;
using System.Text.RegularExpressions;

namespace BlackJackV2.ViewModels
{
	public class StatsViewModel : ReactiveObject
	{
		private readonly Regex InputBetRegex = new Regex(@"^\d+$");
		private IPlayer<Bitmap, string> _currentPlayer;

		private int _funds;
		private bool _isBetEnabled;
		
		
		public IPlayer<Bitmap, string> CurrentPlayer
		{
			get => _currentPlayer;
			set => this.RaiseAndSetIfChanged(ref _currentPlayer, value);
		}

		public int Funds
		{
			get => _funds;
			set => this.RaiseAndSetIfChanged(ref _funds, value);
		}

		public bool IsBetEnabled
		{
			get => _isBetEnabled;
			set => this.RaiseAndSetIfChanged(ref _isBetEnabled, value);
		}

		public ReactiveCommand<string, Unit> InputBetCommand { get; }

		private readonly CompositeDisposable _disposable = new CompositeDisposable();

		public StatsViewModel(IGameCoordinator<Bitmap, string> gameCoordinator, IGameRules<Bitmap, string> gameRule)
		{
			IsBetEnabled = true;

			InputBetCommand = ReactiveCommand.Create<string>(betString =>
			{
				// Ignore Enter key press if the bet is not enabled
				if (!IsBetEnabled) return;

				// Validates the bet input. Must be a number between 1 and 10, and less than or equal to Funds 
				if ( !string.IsNullOrWhiteSpace(betString) && 
						InputBetRegex.IsMatch(betString) &&
						int.TryParse(betString, out int parsedBet))
				{
					var result = gameRule.CanPlaceInitialBet(CurrentPlayer, parsedBet);
					if (!result.IsAllowed)
					{
						//TODO: Show the user that the bet is not allowed
						Debug.WriteLine(result.Message);
					}
					else
					{
						gameCoordinator.OnBetInputReceived(CurrentPlayer.Name, parsedBet);
					}
				}
				else
				{
					// TODO: IMPLEMENT proper error handling
					Debug.WriteLine("Invalid input: not a number.");
				}
			});

			// Set the current player to the one that is requesting a bet
			gameCoordinator.BetRequestedEvent
				.Subscribe(currentPlayer =>{

					CurrentPlayer = currentPlayer;
					// Set the funds to the current players funds
					Funds = CurrentPlayer.Funds;

				}).DisposeWith(_disposable);

			// This will automatically update bet in the UI due to data binding
			gameCoordinator.GameStateObservable
				.Subscribe(gameState =>	{
					IsBetEnabled = !gameState.IsBetRecieved;
			}).DisposeWith(_disposable);
		}

		public void Dispose() => _disposable.Dispose();
	}
}
