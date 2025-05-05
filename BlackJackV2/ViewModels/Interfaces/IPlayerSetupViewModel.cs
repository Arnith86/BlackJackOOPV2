// Project: BlackJackV2
// File: BlackJackV2/ViewModels/Interfaces/IPlayerSetupViewModel.cs

using BlackJackV2.Models.Player;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Reactive;

namespace BlackJackV2.ViewModels.Interfaces
{
	/// <summary>
	/// Represents the interface for the view model that controls game initialization and player setup.
	/// </summary>
	public interface IPlayerSetupViewModel : IDisposable
	{
		/// <summary>
		/// Gets the command used to start a new game.
		/// </summary>
		ReactiveCommand<Unit, Unit> StartNewGameCommand { get; }

		/// <summary>
		/// Gets the list of selectable player count options.
		/// </summary>
		ObservableCollection<int> NumberOptions { get; }

		/// <summary>
		/// Gets the collection of player name entries.
		/// </summary>
		ObservableCollection<PlayerNameEntry> PlayerNames { get; }

		/// <summary>
		/// Gets or sets the number of players selected by the user.
		/// </summary>
		int NumberOfPlayers { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether a new game can be started.
		/// </summary>
		bool CanStartNewGame { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether a new game has started.
		/// </summary>
		bool NewGameStarted { get; set; }
	}
}