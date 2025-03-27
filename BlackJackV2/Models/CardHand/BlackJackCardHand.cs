using Avalonia.Media.Imaging;
using Avalonia.Platform;
using BlackJackV2.Models.CardFactory;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackV2.Models.CardHand
{
	/**
	 * Card Hand 
	 * 
	 * HandValue:			Get the current integer value of hand. 
	 * Hand:				Get the list of card objects of hand
	 * AddCard():			Adds a new card object to the hand 
	 * RemoveCard():		Removes a specific card from hand
	 * ClearHand():			Emptys the hand
	 * CalculateAceValue():	Calculate the current hands value, while accounting for that ace can have a value of either 1 or 11 
	 * 
	 **/

	public class BlackJackCardHand : ICardHand<Bitmap, Bitmap, string>
	{
		private int _handValue;
		public int HandValue => _handValue;

		//public List<ICard<Bitmap, Bitmap, string>> Hand { get; private set; }
		public ObservableCollection<ICard<Bitmap, Bitmap, string>> Hand { get; private set; }


		public BlackJackCardHand()
		{
			_handValue = 0;
			//Hand = new List<ICard<Bitmap, Bitmap, string>>();
			Hand = new ObservableCollection<ICard<Bitmap, Bitmap, string>>();

			// When the hand is changed (card added, removed or hand cleard), the hand value is recalculated
			Hand.CollectionChanged += (sender, e) => _handValue = CalculateHandValue();
		}

		public void AddCard(ICard<Bitmap, Bitmap, string> card)
		{
			Hand.Add(card);
		}

		public void ClearHand()
		{
			Hand.Clear();
		}

		public void RemoveCard(string cardValue)
		{
			//Hand.RemoveAll(card => card.Value == cardValue);
			ICard<Bitmap, Bitmap, string> cardsToRemove = Hand.FirstOrDefault(card => card.Value == cardValue);
			
			if (cardsToRemove != null) Hand.Remove(cardsToRemove);
		}

		// Calculate the current hands value, while accounting for that ace can have a value of either 1 or 11 
		public int CalculateHandValue()
		{
			// Hand is empty, value is zero
			if (Hand.Count == 0) return 0;

			int currentHandValue = 0;
			int aceCount = 0;

			// Sums the values of the current except for aces which it counts.
			foreach (ICard<Bitmap, Bitmap, string> card in Hand)
			{
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
