// Project: BlackJackV2
// file: BlackJackV2/Models/Player/PlayerHands.cs

/// <summary>
///		 Handles a player's card hands in a blackjack game.
///		 When hands are reset, only the primary hand is kept
///		 
///		HandOwner.HandOwner						Id                  : The id of the hand, used to identify the hand in the game
///		BlackJackCardHand						_PrimeryCardHand    : The player's primary hand
///		BlackJackCardHand						_splitCardHand      : The player's split hand
///		Dictionary<HandOwners.HandOwner, int>	Bet					: A Dictionary with bets for the primary and split hands
///		
///		int		GetBetFromHand()							: Returns the bet for the specified hand
///		void	SetBetToHand()								: Sets the bet for the specified hand
///		bool	TryDoubleDownBet(int, IBlackJackCardHand)	:
///		bool	TrySplitHand()								: Splits a hand into two hands. The card chosen for the split is removed and placed in a new hand
///		void	AddCardToHand(IBlackJackCardHand, ICard)	: Adds a new card object to the specified hand
///		void	FoldHand(IBlackJackCardHand)				: Folds the specified hand
///		void	ResetHand()									: Resets hands for a new round
/// </summary>


using Avalonia.Media.Imaging;
using BlackJackV2.Constants;
using BlackJackV2.Factories.CardHandFactory;
using BlackJackV2.Models.Card;
using BlackJackV2.Models.CardHand;
using System;
using System.Collections.Generic;

namespace BlackJackV2.Models.PlayerHands
{
	public class BlackJackPlayerHands : IBlackJackPlayerHands<Bitmap, string>
	{
		// The id of the hand, used to identify the hand in the game
		public HandOwners.HandOwner Id { get; private set; }
		
		// Represents the player's primary hand and split hand
		private IBlackJackCardHand<Bitmap, string> _primeryCardHand;
		private IBlackJackCardHand<Bitmap, string> _splitCardHand;

		public IBlackJackCardHand<Bitmap, string> PrimaryCardHand => _primeryCardHand;
		public IBlackJackCardHand<Bitmap, string> SplitCardHand => _splitCardHand;

		// The bet for the primary and split hands
		public Dictionary<HandOwners.HandOwner, int> Bet;

		

		public BlackJackPlayerHands(HandOwners.HandOwner id, /*IBlackJackCardHand<Bitmap, string> cardHand*/ CardHandCreator<Bitmap, string> cardHandCreator) 
		{
			Id = id;
			
			Bet = new Dictionary<HandOwners.HandOwner, int> 
			{ 
				{ HandOwners.HandOwner.Primary, 0 }, 
				{ HandOwners.HandOwner.Split, 0 } 
			};


			_primeryCardHand = cardHandCreator.CreateCardHand();//(BlackJackCardHand)cardHand;
			_splitCardHand = cardHandCreator.CreateCardHand();// BlackJackCardHandCreator.CreateBlackJackCardHand();

			// Set the id of the primary and split hands if the player is the "Player"
			if (Id == HandOwners.HandOwner.Player)
			{
				_primeryCardHand.Id = HandOwners.HandOwner.Primary;
				_splitCardHand.Id = HandOwners.HandOwner.Split;
			}
		}

		// Returns the bet for the specified hand
		public int GetBetFromHand(HandOwners.HandOwner owner)
		{
			if (Bet.ContainsKey(owner))
				return Bet[owner];
			else
				throw new ArgumentException("Invalid hand owner");
		}

		// Sets the bet for the specified hand
		// Notifies the bet update subject when the bet is updated
		public void SetBetToHand(HandOwners.HandOwner owner, int bet)
		{
			if (Bet.ContainsKey(owner))
			{
				Bet[owner] = bet;
			}
			else
			{
				throw new ArgumentException("Invalid hand owner");
			}
		}

		public bool TryDoubleDownBet(int points, IBlackJackCardHand<Bitmap, string> cardHand)
		{
			// Check if the player has enough funds to double down
			bool enoughFunds = GetBetFromHand(cardHand.Id) <= points;

			// Checks if the hand has two cards
			if (enoughFunds && cardHand.Hand.Count == 2)
			{
				// Doubles the bet for the hand
				SetBetToHand(cardHand.Id, GetBetFromHand(cardHand.Id) * 2);
				Bet[HandOwners.HandOwner.Primary] *= 2;
				return true;
			}
			return false;
		}

		// Splits a hand into two hands. The card chosen for the split is removed and placed in a new hand
		// If successfull, returns true
		public bool TrySplitHand()
		{
			// Checks if the primary hand has two cards, and if the split hand is empty
			if (_primeryCardHand.Hand.Count == 2 && _splitCardHand.Hand.Count < 1 )
			{
				// Retrieves the value of the first two cards in the primary hand
				string value1 = _primeryCardHand.Hand[0].Value.Split('_')[1];
				string value2 = _primeryCardHand.Hand[1].Value.Split('_')[1];

				// Checks to see if the numeric value of the cards are the same 
				if (value1 == value2)
				{
					// Splits the hand
					_splitCardHand.AddCard(_primeryCardHand.Hand[1]);
					_primeryCardHand.RemoveCard(_primeryCardHand.Hand[1].Value);
					// Copy the bet from the primary hand to the split hand
					Bet[HandOwners.HandOwner.Split] = Bet[HandOwners.HandOwner.Primary];

					return true;
				}
			}
			
			return false;
		}

		// Adds a new card object to the specified hand
		public void AddCardToHand(IBlackJackCardHand<Bitmap, string> cardHand, ICard<Bitmap, string> card)
		{
			if (cardHand.Id == HandOwners.HandOwner.Primary)
				_primeryCardHand.AddCard(card);
			else
				_splitCardHand.AddCard(card);
		}

		// Folds the specified hand
		public void FoldHand(IBlackJackCardHand<Bitmap, string> cardHand)
		{
			if (cardHand.Id == HandOwners.HandOwner.Primary)
				_primeryCardHand.IsFolded = true;
			else
				_splitCardHand.IsFolded = true;
		}

		// Resets hands for a new round
		public void ResetHand()
		{
			// Emptys the hands
			_primeryCardHand.ClearHand();
			_splitCardHand.ClearHand();
			// Reset the bets for both hands
			Bet[HandOwners.HandOwner.Primary] = Bet[HandOwners.HandOwner.Split] = 0;
			// Reset the folded state of the hands
			_primeryCardHand.IsFolded = false;
			_splitCardHand.IsFolded = false;
		}
	}
}
