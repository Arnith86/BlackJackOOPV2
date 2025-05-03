// Project: BlackJackV2
// file: BlackJackV2/ViewModels/Interfaces/IButtonViewModel.cs

using System.Reactive;
using ReactiveUI;

namespace BlackJackV2.ViewModels.Interfaces
{
	/// <summary>
	/// Represents the interface for a button-based ViewModel 
	/// that exposes commands for player actions in Blackjack.
	/// </summary>
	/// /// <remarks>
	/// Related files <see cref="BlackJackV2.Factories.ButtonViewModelFactory"/>
	/// </remarks>
	public interface IButtonViewModel
	{
		/// <summary>
		/// Command to execute the Hit action.
		/// </summary>
		ReactiveCommand<Unit, Unit> HitCommand { get; }

		/// <summary>
		/// Command to execute the Fold action.
		/// </summary>
		ReactiveCommand<Unit, Unit> FoldCommand { get; }

		/// <summary>
		/// Command to execute the Double Down action.
		/// </summary>
		ReactiveCommand<Unit, Unit> DoubleDownCommand { get; }

		/// <summary>
		/// Command to execute the Split action.
		/// </summary>
		ReactiveCommand<Unit, Unit> SplitCommand { get; }

		/// <summary>
		/// Indicates whether the hand which this <see cref="IButtonViewModel"/> is bound to is active.
		/// </summary>
		public bool HandIsActive { get; set; }

		/// <summary>
		/// Disposes of the ViewModel and any resources it holds.
		/// </summary>
		public void Dispose();
	}
}