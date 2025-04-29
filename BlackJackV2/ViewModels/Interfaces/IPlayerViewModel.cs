// Project: BlackJackV2
// File: BlackJackV2/ViewModels/Interfaces/IPlayerViewModel.cs

using Avalonia.Media.Imaging;
using BlackJackV2.Models.Player;
using System;
using System.Collections.ObjectModel;

namespace BlackJackV2.ViewModels.Interfaces
{
	/// <summary>
	/// Represents a view model for a Blackjack player, exposing information about hands, funds,
	/// and methods for managing split state and synchronizing bets.
	/// </summary>
	/// /// <remarks>
	/// Related files <see cref="BlackJackV2.Factories.PlayerViewModelFactory"/>
	/// </remarks>
	public interface IPlayerViewModel : IDisposable
	{
		/// <summary>
		/// The underlying player model.
		/// </summary>
		IPlayer<Bitmap, string> Player { get; }

		/// <summary>
		/// The current funds available to the player.
		/// This value is kept in sync with the model.
		/// </summary>
		int Funds { get; set; }

		/// <summary>
		/// View model representing the player’s primary hand.
		/// </summary>
		ICardHandViewModel PlayerCardHandViewModel { get; }

		/// <summary>
		/// View model representing the player’s split hand.
		/// </summary>
		ICardHandViewModel PlayerSplitCardHandViewModel { get; }

		/// <summary>
		/// Collection of card hand view models to be displayed in the UI.
		/// Typically contains one or two elements (primary and optionally split hand).
		/// </summary>
		ObservableCollection<ICardHandViewModel> PlayerCardViewModels { get; }

		/// <summary>
		/// Synchronizes the bet values for the player’s hands, based on the player's name.
		/// </summary>
		/// <param name="playerName">The name of the player whose bets should be synchronized.</param>
		void SyncPlayerBet(string playerName);

		/// <summary>
		/// Adds the split hand view model to the list of views (when the player has split).
		/// </summary>
		void OnPlayerSplit();

		/// <summary>
		/// Removes the split hand view model from the list of views (when the split ends).
		/// </summary>
		void OnPlayerSplitEnd();
	}
}
