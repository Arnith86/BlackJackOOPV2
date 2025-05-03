// Project: BlackJackV2
// file: BlackJackV2/ViewModels/Interfaces/IBetViewModel.cs

using ReactiveUI;
using System.Reactive;

namespace BlackJackV2.ViewModels.Interfaces
{
	/// <summary>
	/// Interface for a view model that handles bet input logic in the Blackjack game.
	/// </summary>
	/// <remarks>
	/// Related files <see cref="BlackJackV2.Factories.BetViewModelFactory"/>
	/// </remarks>
	public interface IBetViewModel
	{
		/// <summary>
		/// Command triggered when the user inputs a bet.
		/// Validates and processes the bet input.
		/// </summary>
		ReactiveCommand<string, Unit> InputBetCommand { get; }

		/// <summary>
		/// Indicates whether the player is allowed to place a bet.
		/// Used to show or hide betView.
		/// </summary>
		bool CanPlaceBet { get; set; }

		/// <summary>
		/// Disposes of the resources used by the BetViewModel.
		/// </summary>
		public void Dispose();
	}
}