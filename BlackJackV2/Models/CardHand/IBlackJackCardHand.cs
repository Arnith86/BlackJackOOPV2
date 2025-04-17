// Project: BlackJackV2
// file: BlackJackV2/Models/CardHand/BlackJackCardHandCreator.cs

/// <summary>
///		Card Hand interface representing a single card hand in a blackjack game.
///		Basic funtionallity present
/// 
///		HandOwners.HandOwner			Id			: The id of the hand, used to identify the hand in the game
///		bool							IsActive	: True if the hand is active
///		ObservableCollection<ICard<>>	Hand		: Get the list of card objects of hand
///		int								HandValue	: Get the current integer value of hand. 
///		bool							IsBlackJack	: True if card hand is black jack (21 and 2 cards)
///		bool							IsBusted	: True if card hand is busted (value > 21)
///		bool							IsFolded		: True if card hand is folded (value == 0)
///		
///		void		AddCard()		: Adds a new card object to the hand 
///		void		RemoveCard()	: Removes a specific card from hand 
///		void		ClearHand()		: Emptys the hand
///		
/// </summary>

using BlackJackV2.Constants;
using System.Collections.ObjectModel;

namespace BlackJackV2.Models.CardHand
{
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
