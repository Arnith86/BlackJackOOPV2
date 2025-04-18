﻿// Project: BlackJackV2
// file: BlackJackV2/Models/CardHand/BlackJackCardHand.cs

/// <summary>
/// 
///		This class represents a single card hand in a blackjack game.
///		
///		HandOwners.HandOwner	Id					: The id of the hand, used to identify the hand in the game
///		bool					IsActive			: True if the hand is active
///		
///		ObservableCollection	Hand				: Get the list of card objects of hand
///		int						HandValue			: Get the current integer value of hand. 
///		bool					IsBlackJack			: True if card hand is black jack (21 and 2 cards)
///		bool					IsBusted			: True if card hand is busted (value > 21)
///		bool					IsFolded			: Is set from outside the class
///		
///		void					AddCard()			: Adds a new card object to the hand 
///		void					RemoveCard()		: Removes a specific card from hand
///		void					ClearHand()			: Emptys the hand
///		int						CalculateAceValue()	: Calculate the current hands value, while accounting for that ace can have a value of either 1 or 11 
/// 
/// </summary>

using Avalonia.Media.Imaging;
using BlackJackV2.Constants;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Linq;

namespace BlackJackV2.Models.CardHand
{
	public class BlackJackCardHand : ReactiveObject, IBlackJackCardHand<Bitmap, string> 
	{
		// The id of the hand, used to identify the hand in the game
		public HandOwners.HandOwner Id { get; set; }

		// True if the hand is active
		private bool _isActive = false;
		public bool IsActive 
		{
			get => _isActive;
			set => this.RaiseAndSetIfChanged(ref _isActive, value);
		} 

		// The cards in the hand
		public ObservableCollection<ICard<Bitmap, string>> Hand { get; private set; }

		// The current integer value of hand.
		private int _handValue;
		public int HandValue => _handValue;

		// True if card hand is black jack (21 and 2 cards)
		public bool IsBlackJack => _handValue == 21 && Hand.Count == 2;
		
		// True if card hand is busted (value > 21)
		public bool IsBusted => _handValue > 21;

		// Is set from outside the class or if _handValue = 21, if true, the hand is folded
		private bool _isFolded = false;
		public bool IsFolded
		{
			get => _isFolded;
			set => _isFolded = value;
		} 

		public BlackJackCardHand()
		{	
			_handValue = 0;
			Hand = new ObservableCollection<ICard<Bitmap, string>>();

			// When the hand is changed (card added, removed or hand cleard), the hand value is recalculated
			Hand.CollectionChanged += (sender, e) => 
			{ 
				_handValue = CalculateHandValue();
				if (_handValue == 21) IsFolded = true;
			};
		}

		public void AddCard(ICard<Bitmap, string> card)
		{
			Hand.Add(card);
		}

		public void RemoveCard(string cardValue)
		{
			ICard<Bitmap, string> cardsToRemove = Hand.FirstOrDefault(card => card.Value == cardValue);
			
			if (cardsToRemove != null) Hand.Remove(cardsToRemove);
		}

		public void ClearHand()
		{
			Hand.Clear();
		}

		// Calculate the current hands value, while accounting for that ace can have a value of either 1 or 11 
		public int CalculateHandValue()
		{
			// Hand is empty, value is zero
			if (Hand.Count == 0) return 0;

			int currentHandValue = 0;
			int aceCount = 0;

			// Sums the values of the current except for aces which it counts.
			foreach (ICard<Bitmap,string> card in Hand)
			{
				// If card is face down, skip it
				if (card.FaceDown == true) continue;

				// Seperates int value and suite
				string[] valueString = card.Value.Split('_');

				// If king, Queen or Knight then value = 10, all other values (excluding ace) keep their value
				if (int.TryParse(valueString[1], out int value) && value == 1) aceCount++;
				else currentHandValue += value > 10 ? 10 : value;
			}


			// Ace card(s) are present in hand
			/**
			 *	Ace recives the value of 11 iff the total value of the hand does not exceed the value of 21. 
			 *	When checking an ace value and there are more aces present in the hand, then 
			 *	(currentValueOfHand + 11 + numberOfAcesLeft) cannot exceed 21, otherwise ace gets the value 1.
			 */
			if (aceCount > 0)
			{
				while (aceCount > 0)
				{
					aceCount--;
					int handValueAttempt = currentHandValue + 11;

					if (handValueAttempt <= 21 && handValueAttempt + aceCount <= 21) currentHandValue += 11;
					else currentHandValue++;
				}
			}

			return currentHandValue;
		}
	}
}
