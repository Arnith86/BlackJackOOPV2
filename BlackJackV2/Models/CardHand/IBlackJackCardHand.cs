// Project: BlackJackV2
// file: BlackJackV2/Models/CardHand/BlackJackCardHandCreator.cs

using BlackJackV2.Constants;
using BlackJackV2.Models.Card;
using System.Collections.ObjectModel;

namespace BlackJackV2.Models.CardHand
{
	/// <summary>
	/// Represents a player's hand in a Blackjack game, managing its cards and state.
	/// This interface serves as the product in the CardHand Factory.
	/// Provides functionality to add, remove, and clear cards, and tracks whether the hand is active, busted, a blackjack, or folded.
	/// </summary>
	/// <typeparam name="TImage">The type representing the image used on the card (e.g., bitmap).</typeparam>
	/// <typeparam name="TValue">The type representing the card's value (e.g., string, int).</typeparam>
	public interface IBlackJackCardHand<TImage, TValue>
	{

		/// <summary>
		/// Gets or sets the unique identifier for the hand, used to associate the hand with either primary or split hand.
		/// </summary>
		public HandOwners.HandOwner Id { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether the hand is currently active in gameplay.
		/// </summary>
		public bool IsActive { get; set; }


		/// <summary>
		/// Gets the collection of cards currently in the hand.
		/// </summary>
		public ObservableCollection<ICard<TImage, TValue>> Hand { get; }

		/// <summary>
		/// Gets the total current numerical value of the hand.
		/// </summary>
		public int HandValue { get; }

		/// <summary>
		/// Gets a value indicating whether the hand is a BlackJack (exactly 21 with two cards).
		/// </summary>
		public bool IsBlackJack { get; }


		/// <summary>
		/// Gets a value indicating whether the hand is busted (value excceds 21).
		/// </summary>
		public bool IsBusted { get; }

		/// <summary>
		/// Gets a value indicating whether the hand is folded (intentionally withdrawn).
		/// </summary>
		public bool IsFolded { get; set; }

		/// <summary>
		/// Adds a card to the hand.
		/// </summary>
		/// <param name="card">The card to add.</param>
		public void AddCard(ICard<TImage, TValue> card);

		/// <summary>
		/// Removes a specific card from the hand by its value.
		/// </summary>
		/// <param name="cardValue">The value of the card to remove.</param>
		public void RemoveCard(string cardValue);

		/// <summary>
		/// Clears the hand of any cards, reseting its state.
		/// </summary>
		public void ClearHand();

		/// <summary>
		/// Used to manually recalculate the hand value after card flip.
		/// </summary>
		public void RecalculateHandAfterCardFlip();
	}
}
