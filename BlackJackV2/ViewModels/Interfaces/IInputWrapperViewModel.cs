// Project: BlackJackV2
// File: BlackJackV2/ViewModels/Interfaces/IInputWrapperViewModel.cs

using BlackJackV2.ViewModels.Interfaces;

namespace BlackJackV2.ViewModels.Interfaces
{
	/// <summary>
	/// Defines a contract for a view model that encapsulates both bet input and action buttons
	/// used during player interaction in a Blackjack game.
	/// </summary>
	public interface IInputWrapperViewModel
	{
		/// <summary>
		/// Gets the view model that handles the player's action buttons (e.g., Hit, Stand).
		/// </summary>
		IButtonViewModel ButtonViewModel { get; }

		/// <summary>
		/// Gets the view model that handles the player's betting input.
		/// </summary>
		IBetViewModel BetViewModel { get; }
	}
}