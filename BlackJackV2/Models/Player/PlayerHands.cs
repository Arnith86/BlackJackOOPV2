using Avalonia.Controls.Shapes;
using Avalonia.Media.Imaging;
using BlackJackV2.Constants;
using BlackJackV2.Models.CardHand;
using BlackJackV2.Services.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackV2.Models.Player
{
	/**
	 *	Handles a player's card hands in a blackjack game.
	 *	When hands are reset, only the primary hand is kept 
	 *
	 *  Id					: The id of the hand, used to identify the hand in the game
	 *	_PrimeryCardHand	: The player's primary hand
	 *	_splitCardHand		: The player's split hand
	 *	Bet					: A Dictionary with bets for the primary and split hands
	 *	_betUpdateSubject	: The subject used to notify when the bet is updated
	 *	
	 *	GetBetFromHand()	: Returns the bet for the specified hand
	 *	SetBetToHand()		: Sets the bet for the specified hand
	 *	SplitHand()			: Splits a hand into two hands. The card chosen for the split is removed and placed in a new hand
	 *	ResetHand()			: Empteys all hands of cards
	 */

	public class PlayerHands : IPlayerHands<Bitmap, string>
	{
		// The id of the hand, used to identify the hand in the game
		public HandOwners.HandOwner Id { get; private set; }
		
		// Represents the player's primary hand and split hand
		private BlackJackCardHand _primeryCardHand;
		private BlackJackCardHand _splitCardHand;

		public BlackJackCardHand PrimaryCardHand => _primeryCardHand;
		public BlackJackCardHand SplitCardHand => _splitCardHand;

		// The bet for the primary and split hands
		public Dictionary<HandOwners.HandOwner, int> Bet;

		// The subject used to notify when the bet is updated
		private readonly ISubject<BetUpdateEvent> _betUpdateSubject;

		public PlayerHands(ISubject<BetUpdateEvent> betUpdateSubject, HandOwners.HandOwner id, IBlackJackCardHand<Bitmap, string> cardHand) 
		{
			Id = id;
			
			Bet = new Dictionary<HandOwners.HandOwner, int> 
			{ 
				{ HandOwners.HandOwner.Primary, 0 }, 
				{ HandOwners.HandOwner.Split, 0 } 
			};

			_betUpdateSubject = betUpdateSubject;
			
			_primeryCardHand = (BlackJackCardHand)cardHand;
			_splitCardHand = BlackJackCardHandCreator.CreateBlackJackCardHand();

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
				_betUpdateSubject.OnNext(new BetUpdateEvent(this));
			}
			else
			{
				throw new ArgumentException("Invalid hand owner");
			}
		}

		// Splits a hand into two hands. The card chosen for the split is removed and placed in a new hand
		public bool SplitHand()
		{
			// Checks if the primary hand has two cards
			if (_primeryCardHand.Hand.Count == 2 && _splitCardHand.Hand.Count < 1)
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

		// Empties all hands of cards
		public void ResetHand()
		{
			// Emptys the hands
			_primeryCardHand.ClearHand();
			_splitCardHand.ClearHand();
			// Reset the bets for both hands
			Bet[HandOwners.HandOwner.Primary] = Bet[HandOwners.HandOwner.Split] = 0;
		}
	}
}
