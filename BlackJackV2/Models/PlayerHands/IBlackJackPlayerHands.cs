// Project: BlackJackV2
// file: BlackJackV2/Models/Player/IPlayerHands.cs

using BlackJackV2.Models.CardHand;
using BlackJackV2.Models.Card;
using BlackJackV2.Shared.Constants;

namespace BlackJackV2.Models.PlayerHands
{
	/// <summary>
	///	Interface for the player hands wrapper class. Used as a container for the primary and split hands, and the bets related to them. It also handles which hands that an action is performed on.
	///	Serves as the product in the PlayerHands Factory pattern.
	///	</summary>
	/// <remarks>
	/// Related files: <see cref = "BlackJackV2.Factories.PlayerHandsFactory" />
	/// </remarks>
	public interface IBlackJackPlayerHands<TImage, TValue>
	{
		/// <summary>
		/// Gets the primary hand or the Player.
		/// </summary>
		public IBlackJackCardHand<TImage, TValue> PrimaryCardHand { get; }

		/// <summary>
		/// Gets the split hand of the Player.
		/// </summary>
		public IBlackJackCardHand<TImage, TValue> SplitCardHand { get; }

		/// <summary>
		/// Gets the current bet associated with hand owner.
		/// </summary>
		/// <param name="owner">The owner of the hand (primary or split).</param>
		/// <returns>The current bet.</returns>
		public int GetBetFromHand(HandOwners.HandOwner owner);

		/// <summary>
		/// Sets the bet for the specified hand owner.
		/// </summary>
		/// <param name="owner">The owner of the card hand (primary or split).</param>
		/// <param name="bet">The supplied bet.</param>
		public void SetBetToHand(HandOwners.HandOwner owner, int bet);

		/// <summary>
		/// Attempts to double down the bet for the specified card hand.
		/// </summary>
		/// <param name="cardHand">The hand associated with the double down attempt.</param>
		/// <returns>True, if the attempt was successfull, false, if not.</returns>
		public bool TryDoubleDownBet(IBlackJackCardHand<TImage, TValue> cardHand);

		/// <summary>
		/// Attempts to split the players primary hand into two hands.
		/// </summary>
		/// <returns>The primary and split <see cref="BlackJackCardHand{TImage, TValue}"/>.</returns>
		public (IBlackJackCardHand<TImage, TValue> primary, IBlackJackCardHand<TImage, TValue> split) SplitHand();

		/// <summary>
		/// Adds a new <see cref="ICard{TImage, TValue}"/> to the specified hand.
		/// </summary>
		/// <param name="cardHand">The <see cref="IBlackJackCardHand{TImage, TValue}"/> which is to receive a new <see cref="ICard{TImage, TValue}"/>.</param>
		/// <param name="card">The <see cref="ICard{TImage, TValue}"/> to add.</param>
		public void AddCardToHand(IBlackJackCardHand<TImage, TValue> cardHand, ICard<TImage, TValue> card);

		/// <summary>
		/// Folds the specified hand, marking it as inactive.
		/// </summary>
		/// <param name="cardHand">The specified <see cref="IBlackJackCardHand{TImage, TValue}"/> to fold.</param>
		public void FoldHand(IBlackJackCardHand<TImage, TValue> cardHand);

		/// <summary>
		/// Resetting the hands for a new round. This includes clearing the hands and resetting the bets.
		/// </summary>
		public void ResetHand();
		
	}
}
