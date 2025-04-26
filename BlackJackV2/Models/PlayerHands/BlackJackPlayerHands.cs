// Project: BlackJackV2
// file: BlackJackV2/Models/Player/PlayerHands.cs

using BlackJackV2.Factories.CardHandFactory;
using BlackJackV2.Models.Card;
using BlackJackV2.Models.CardHand;
using BlackJackV2.Shared.Constants;
using BlackJackV2.Shared.UtilityClasses;
using System;
using System.Collections.Generic;

namespace BlackJackV2.Models.PlayerHands
{
	/// <summary>
	///	This class is used as a wrapper for the <see cref="IBlackJackCardHand{TImage, TValue}"/> used as primary and split hands. 
	///	It handles the bets related to each card hand and which hands that an action is performed on.
	///	Serves as the product in the PlayerHands Factory pattern.
	/// </summary>
	/// <remarks>
	/// Related files: <see cref = "BlackJackV2.Factories.PlayerHandsFactory" />
	/// </remarks>
	public class BlackJackPlayerHands<TImage, TValue> : IBlackJackPlayerHands<TImage, TValue>
	{
		/// <summary>
		/// The identafier of the Player owning the hands, used to identify the hands in the game.
		/// </summary>
		public HandOwners.HandOwner Id { get; private set; }
		
		private IBlackJackCardHand<TImage, TValue> _primaryCardHand;
		/// <inheritdoc/>
		public IBlackJackCardHand<TImage, TValue> PrimaryCardHand => _primaryCardHand;

		private IBlackJackCardHand<TImage, TValue> _splitCardHand;
		/// <inheritdoc/>
		public IBlackJackCardHand<TImage, TValue> SplitCardHand => _splitCardHand;

		/// <summary>
		/// The bets for the primary and split hands
		/// </summary>
		private Dictionary<HandOwners.HandOwner, int> Bet;

		
		/// <summary>
		/// Initializes a new instance of the <see cref="BlackJackPlayerHands"/> class.
		/// </summary>
		/// <param name="id">The identifier of the player owning the hands.</param>
		/// <param name="cardHandCreator"> Creates new <see cref="BlackJackCardHand"/>
		/// </param>
		public BlackJackPlayerHands(HandOwners.HandOwner id, CardHandCreator<TImage, TValue> cardHandCreator) 
		{
			Id = id;
			
			Bet = new Dictionary<HandOwners.HandOwner, int> 
			{ 
				{ HandOwners.HandOwner.Primary, 0 }, 
				{ HandOwners.HandOwner.Split, 0 } 
			};


			_primaryCardHand = cardHandCreator.CreateCardHand();
			_splitCardHand = cardHandCreator.CreateCardHand();

			// Set the id of the primary and split hands if the player is the "Player"
			if (Id == HandOwners.HandOwner.Player)
			{
				_primaryCardHand.Id = HandOwners.HandOwner.Primary;
				_splitCardHand.Id = HandOwners.HandOwner.Split;
			}
		}

		/// <inheritdoc/>
		public int GetBetFromHand(HandOwners.HandOwner owner)
		{
			if (Bet.ContainsKey(owner))
				return Bet[owner];
			else
				throw new ArgumentException("Invalid hand owner");
		}

		///<inheritdoc/>
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

		/// <inheritdoc/>
		public bool TryDoubleDownBet( IBlackJackCardHand<TImage, TValue> cardHand)
		{
			// Checks if the hand has two cards
			if (cardHand.Hand.Count == 2)
			{
				// Doubles the bet for the hand
				SetBetToHand(cardHand.Id, GetBetFromHand(cardHand.Id) * 2);
				return true;
			}
			return false;
		}

		/// <inheritdoc/>
		public (IBlackJackCardHand<TImage, TValue> primary, IBlackJackCardHand<TImage, TValue> split) SplitHand()
		{
			// Retrieves the value of the first two cards in the primary hand
			string value1 = CardToValueUtility<TImage, TValue>.GetNumericCardValue(_primaryCardHand.Hand[0]); 
			string value2 = CardToValueUtility<TImage, TValue>.GetNumericCardValue(_primaryCardHand.Hand[1]); 

			// Splits the hand
			_splitCardHand.AddCard(_primaryCardHand.Hand[1]);
			_primaryCardHand.RemoveCard(_primaryCardHand.Hand[1].Value.ToString());
			// Copy the bet from the primary hand to the split hand
			Bet[HandOwners.HandOwner.Split] = Bet[HandOwners.HandOwner.Primary];

			//splitHands = (_primaryCardHand, _splitCardHand);
			
			return (_primaryCardHand, _splitCardHand);
		}

		/// <summary>
		/// Returns a <see cref="IBlackJackCardHand{Bitmap, string}"/> based on the specified owner.
		/// </summary>
		/// <param name="owner">The owner of the requested hand.</param>
		/// <returns>The <see cref="IBlackJackCardHand{Bitmap, string}"/> requested.</returns>
		private IBlackJackCardHand<TImage, TValue> GetCardHand(HandOwners.HandOwner owner)
		{
			return owner == HandOwners.HandOwner.Primary ? _primaryCardHand : _splitCardHand;
		}

		/// <inheritdoc/>
		public void AddCardToHand(IBlackJackCardHand<TImage, TValue> cardHand, ICard<TImage, TValue> card) =>
			GetCardHand(cardHand.Id).AddCard(card);
		
		/// <inheritdoc/>
		public void FoldHand(IBlackJackCardHand<TImage, TValue> cardHand) =>
			GetCardHand(cardHand.Id).IsFolded = true;

		/// <inheritdoc/>
		public void ResetHand()
		{
			// Emptys the hands
			_primaryCardHand.ClearHand();
			_splitCardHand.ClearHand();
			// Reset the bets for both hands
			Bet[HandOwners.HandOwner.Primary] = Bet[HandOwners.HandOwner.Split] = 0;
			// Reset the folded state of the hands
			_primaryCardHand.IsFolded = false;
			_splitCardHand.IsFolded = false;
		}
	}
}
