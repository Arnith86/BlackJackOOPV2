﻿// Project: BlackJackV2
// file: BlackJackV2/Models/CardHand/BlackJackCardHand.cs

using BlackJackV2.Models.Card;
using BlackJackV2.Shared.Constants;
using BlackJackV2.Shared.UtilityClasses;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Linq;

namespace BlackJackV2.Models.CardHand
{
	/// <summary>
	/// Represents a hand of cards in a Blackjack game, handling card collection and rule-based state (e.g., bust, blackjack, folded).
	/// Automatically recalculates the hand value when cards are added or removed.
	/// </summary>
	/// <remarks> Related files <see cref="BlackJackV2.Factories.CardHandFactory"/></remarks>
	public class BlackJackCardHand<TImage, TValue> : ReactiveObject, IBlackJackCardHand<TImage, TValue>
	{
		/// <inheritdoc/>
		public HandOwners.HandOwner Id { get; set; }

		private bool _isActive = false;
		/// <inheritdoc/>
		public bool IsActive 
		{
			get => _isActive;
			set => this.RaiseAndSetIfChanged(ref _isActive, value);
		}
		
		/// <inheritdoc/>
		public ObservableCollection<ICard<TImage, TValue>> Hand { get; private set; }

		private int _handValue;
		/// <inheritdoc/>
		public int HandValue
		{
			get => _handValue;
			set => this.RaiseAndSetIfChanged(ref _handValue, value);
		} 

		/// <inheritdoc/>
		public bool IsBlackJack => _handValue == 21 && Hand.Count == 2;

		/// <inheritdoc/>
		public bool IsBusted => _handValue > 21;

		
		private bool _isFolded = false;
		/// <inheritdoc/>
		public bool IsFolded
		{
			get => _isFolded;
			set => _isFolded = value;
		}

		/// <summary>
		/// Initializes a new instance of <see cref="BlackJackCardHand"/> with an empty hand.
		/// Automatically updates the hand value when cards are added, removed, or cleared.
		/// </summary>
		public BlackJackCardHand()
		{	
			HandValue = 0;
			Hand = new ObservableCollection<ICard<TImage, TValue>>();

			// When the hand is changed (card added, removed or hand cleard), the hand value is recalculated
			Hand.CollectionChanged += (sender, e) => 
			{
				HandValue = CalculateHandValue();
				if (_handValue == 21) IsFolded = true;
			};
		}


		/// <inheritdoc/>
		public void AddCard(ICard<TImage, TValue> card)
		{
			if ( card != null)
				Hand.Add(card);
		}

		/// <inheritdoc/>
		public void RemoveCard(string cardValue)
		{
			ICard<TImage, TValue> cardsToRemove = Hand.FirstOrDefault(card => card.Value.ToString() == cardValue);
			if (cardsToRemove != null) 
				Hand.Remove(cardsToRemove);
		}

		/// <inheritdoc/>
		public void ClearHand() => Hand.Clear();

		/// <inheritdoc/>
		public void RecalculateHandAfterCardFlip()
		{
			HandValue = CalculateHandValue();
		}

		/// <summary>
		/// Calculates the total value of the hand, considering Blackjack rules for face cards and aces (ace can have a value of either 1 or 11).
		/// </summary>
		/// <returns>The computed integer value of the current hand.</returns>
		private int CalculateHandValue()
		{
			// Hand is empty
			if (Hand.Count == 0) return 0;

			int total = 0;
			int aceCount = 0;

			// Sums the values of the current except for aces which it counts.
			foreach (ICard<TImage, TValue> card in Hand)
			{
				// Face down card are not counted
				if (card.FaceDown == true) continue;

				// Get the value of the card. 
				string valueString = CardToValueUtility<TImage, TValue>.GetNumericCardValue(card);

				// If king, Queen or Knight then value = 10, all other values (excluding ace) keep their value
				if (int.TryParse(valueString, out int value))
				{ 
					if (value == 1) aceCount++;
					else total += value > 10 ? 10 : value;
				}
			}

						
			/**
			 *	Ace recives the value of 11 iff the total value of the hand does not exceed the value of 21. 
			 *	When checking an ace value and there are more aces present in the hand, then 
			 *	(currentValueOfHand + 11 + numberOfAcesLeft) cannot exceed 21, otherwise ace gets the value 1.
			 */
			/// <summary>
			/// Calculates the total value of the hand, considering Blackjack rules for face cards and aces.
			/// </summary>
			/// <returns>
			/// The computed integer value of the current hand.
			/// </returns>
			if (aceCount > 0)
			{
				while (aceCount > 0)
				{
					aceCount--;
					if (total + 11 + aceCount <= 21)
						total += 11;
					else
						total += 1;
				}
			}

			return total;
		}
	}
}
