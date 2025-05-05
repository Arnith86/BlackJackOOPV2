// Project: BlackJackV2
// file: BlackJackV2/ViewModels/InformationViewModel.cs

using Avalonia.Media.Imaging;
using BlackJackV2.Models.GameLogic;
using BlackJackV2.Models.GameLogic.PlayerServices;
using BlackJackV2.Models.Player;
using BlackJackV2.ViewModels.Interfaces;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Disposables;

namespace BlackJackV2.ViewModels
{
	/// <summary>
	/// ViewModel that manages the initial game setup including player count, names, and starting the game.
	/// </summary>
	public class PlayerSetupViewModel : ReactiveObject, IPlayerSetupViewModel, IDisposable
	{
		private bool _canStartNewGame;
		private bool _newGameStarted;
		private int _numberOfPlayers;
		private readonly CompositeDisposable _disposable = new CompositeDisposable();

		/// <summary>
		/// Command that starts a new game when executed.
		/// </summary>
		public ReactiveCommand<Unit, Unit> StartNewGameCommand { get; }

		/// <summary>
		/// The selectable options for the number of players.
		/// </summary>
		public ObservableCollection<int> NumberOptions { get; } = new ObservableCollection<int> {1, 2, 3, 4 };

		/// <summary>
		/// A collection representing player names entered or auto-generated.
		/// </summary>
		public ObservableCollection<PlayerNameEntry> PlayerNames { get; } = new ();

		/// <summary>
		/// Initializes a new instance of the <see cref="InformationViewModel"/> class.
		/// </summary>
		/// <param name="gameLogic">The game logic instance to run the game.</param>
		/// <param name="playerServices">The service responsible for handling player-related events.</param>
		public PlayerSetupViewModel(GameLogic<Bitmap, string> gameLogic, IPlayerServices<Bitmap, string> playerServices)
		{
			_numberOfPlayers = 0;

			StartNewGameCommand = ReactiveCommand.CreateFromTask(async () =>
			{
				CanStartNewGame = false;
				NewGameStarted = true;
				playerServices.OnPlayerChangedReceived(PlayerNames); 
				await gameLogic.RunGameLoop();
			});

			this.WhenAnyValue(x => x.NumberOfPlayers)
				.Subscribe(players =>
				{
					if (players > 0)
					{
						CanStartNewGame = true;
						NewGameStarted = false;
						GenerateDefaultPlayerNames();
					}
				})
				.DisposeWith(_disposable);
		}

		/// <summary>
		/// Generates and assigns default player names based on the current number of players.
		/// </summary>
		private void GenerateDefaultPlayerNames()
		{
			PlayerNames.Clear();

			for (int i = 0; i < _numberOfPlayers; i++)
				PlayerNames.Add(new PlayerNameEntry(i, $"Player {i + 1}"));			 
		}

		/// <inheritdoc/>
		public int NumberOfPlayers
		{
			get => _numberOfPlayers;
			set => this.RaiseAndSetIfChanged(ref _numberOfPlayers, value);
		}

		/// <inheritdoc/>
		public bool CanStartNewGame
		{
			get => _canStartNewGame;
			set => this.RaiseAndSetIfChanged(ref _canStartNewGame, value);
		}

		/// <inheritdoc/>
		public bool NewGameStarted
		{
			get => !_newGameStarted;
			set => this.RaiseAndSetIfChanged(ref _newGameStarted, value);
		}

		/// <summary>
		/// Disposes reactive subscriptions.
		/// </summary>
		public void Dispose() => _disposable.Dispose();
	}
}
