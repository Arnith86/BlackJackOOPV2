using BlackJackV2.Constants;
using BlackJackV2.Models.CardFactory;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackV2.Models.CardHand
{
	/**
	 * Card Hand interface 
	 * Basic funtionallity present
	 * 
	 * Id			: The id of the hand, used to identify the hand in the game
	 * IsActive		: True if the hand is active
	 * Hand			: Get the list of card objects of hand
	 * HandValue	: Get the current integer value of hand. 
	 * IsBlackJack	: True if card hand is black jack (21 and 2 cards)
	 * IsBusted		: True if card hand is busted (value > 21)
	 * IsFolded		: True if card hand is folded (value == 0)
	 * AddCard()	: Adds a new card object to the hand 
	 * RemoveCard()	: Removes a specific card from hand 
	 * ClearHand()	: Emptys the hand
	 * 
	 **/

	public interface IBlackJackCardHand<TImage, TValue>
	{
		public HandOwners.HandOwner Id { get; }
		public bool IsActive { get; set; }
		public ObservableCollection<ICard<TImage, TValue>> Hand { get; }
		public int HandValue { get; }
		public bool IsBlackJack { get; }
		public bool IsBusted { get; }
		public bool IsFolded { get; set; }
		public void AddCard(ICard<TImage, TValue> card);
		public void RemoveCard(string cardValue);
		public void ClearHand();
		
	}
}
