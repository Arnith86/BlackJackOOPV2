using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJackV2.Models.CardHand;
using BlackJackV2.Constants;
using Avalonia.Media.Imaging;

namespace BlackJackV2.Models.Player
{
	/**
	 * Interface for Player hand handeling 
	 **/

	public interface IPlayerHands<TImage, TValue>
	{
		public BlackJackCardHand PrimaryCardHand { get; }
		public BlackJackCardHand SplitCardHand { get; }

		public int GetBetFromHand(HandOwners.HandOwner owner);
		public void SetBetToHand(HandOwners.HandOwner owner, int bet);
		public bool TryDoubleDownBet(int points, IBlackJackCardHand<Bitmap, string> hand);
		public bool TrySplitHand();
		public void AddCardToHand(IBlackJackCardHand<Bitmap, string> cardHand, ICard<Bitmap, string> card);
		public void FoldHand(IBlackJackCardHand<Bitmap, string> cardHand);
		public void ResetHand();
		
	}
}
