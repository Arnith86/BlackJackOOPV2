using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJackV2.Models.CardHand;
using BlackJackV2.Constants;

namespace BlackJackV2.Models.Player
{
	/**
	 * Interface for Player hand handeling 
	 **/

	public interface IPlayerHands<TImage, TValue>
	{
		public HandOwners.HandOwner Id { get; }
		public BlackJackCardHand PrimaryCardHand { get; }
		public BlackJackCardHand SplitCardHand { get; }
		
		public bool SplitHand();
		public void ResetHand();
	}
}
