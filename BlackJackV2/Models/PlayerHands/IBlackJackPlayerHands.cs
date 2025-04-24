// Project: BlackJackV2
// file: BlackJackV2/Models/Player/IPlayerHands.cs

/// <summary>
///		Interface for Player hands handeling 
///		
///		BlackJackCardHand	PrimaryCardHand						: The player's primary hand
///		BlackJackCardHand	SplitCardHand						: The player's split hand
///
///		int					GetBetFromHand(HandOwners.HandOwner)		: Returns the bet for the specified hand
///		void				SetBetToHand(HandOwners.HandOwner, int)		: Sets the bet for the specified hand
///		bool				TryDoubleDownBet(int, IBlackJackCardHand)	: Tries to double down the bet for the specified hand
///		bool				TrySplitHand()								: Tries to split the hand
///		void				AddCardToHand(IBlackJackCardHand, ICard)	: Adds a card to the specified hand
///		void				FoldHand(IBlackJackCardHand)				: Folds the specified hand
///		void				ResetHand()									: Resets all player hands
///		
/// </summary>

using BlackJackV2.Models.CardHand;
using BlackJackV2.Constants;
using Avalonia.Media.Imaging;
using BlackJackV2.Models.Card;

namespace BlackJackV2.Models.PlayerHands
{

	public interface IBlackJackPlayerHands<TImage, TValue>
	{
		public IBlackJackCardHand<Bitmap, string> PrimaryCardHand { get; }
		public IBlackJackCardHand<Bitmap, string> SplitCardHand { get; }
		public int GetBetFromHand(HandOwners.HandOwner owner);
		public void SetBetToHand(HandOwners.HandOwner owner, int bet);
		public bool TryDoubleDownBet(HandOwners.HandOwner handOwner, IBlackJackCardHand<Bitmap, string> cardHand);
		public bool TrySplitHand(out (IBlackJackCardHand<Bitmap, string> primary, IBlackJackCardHand<Bitmap, string> split) splitHands);
		public void AddCardToHand(IBlackJackCardHand<Bitmap, string> cardHand, ICard<Bitmap, string> card);
		public void FoldHand(IBlackJackCardHand<Bitmap, string> cardHand);
		public void ResetHand();
		
	}
}
