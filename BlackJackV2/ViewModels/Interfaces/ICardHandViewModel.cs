// Project: BlackJackV2
// file: BlackJackV2/ViewModels/Interfaces/ICardHandViewModel.cs

using System;
using System.Collections.ObjectModel;
using System.Reactive;
using Avalonia.Media.Imaging;
using BlackJackV2.Models.Card;
using BlackJackV2.Shared.Constants;
using ReactiveUI;

namespace BlackJackV2.ViewModels.Interfaces
{
	/// <summary>
	/// Represents a UI-facing view model for a single card hand in Blackjack,
	/// exposing properties for state, cards, and interactivity.
	/// </summary>
	/// <remarks>
	/// Related files <see cref="BlackJackV2.Factories.CardHandViewModelFactory"/>
	/// </remarks>
	public interface ICardHandViewModel : IDisposable
	{
		/// <summary>
		/// Identifier for the hand (e.g. Primary or Split).
		/// </summary>
		HandOwners.HandOwner Id { get; }

		/// <summary>
		/// Current bet associated with this hand.
		/// </summary>
		int Bet { get; set; }

		/// <summary>
		/// Whether this hand is currently active (e.g., player's turn).
		/// </summary>
		bool HandIsActive { get; set; }

		/// <summary>
		/// Display value of the hand (e.g., "17", "Blackjack").
		/// </summary>
		string HandValue { get; set; }

		/// <summary>
		/// The collection of cards currently held in this hand.
		/// </summary>
		ObservableCollection<ICard<Bitmap, string>> Cards { get; }

		/// <summary>
		/// Encapsulated button logic related to this hand (e.g., actions like Hit, Stand).
		/// </summary>
		IButtonViewModel ButtonViewModel { get; }
	}
}