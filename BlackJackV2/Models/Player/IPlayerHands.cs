using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJackV2.Models.CardHand;

namespace BlackJackV2.Models.Player
{
	/**
	 * Interface for Player hand handeling 
	 **/

	public interface IPlayerHands<TImage, TValue>
	{
		public IBlackJackCardHand<TImage, TValue> PrimaryCardHand { get; }
		public IBlackJackCardHand<TImage, TValue> SplitCardHand { get; }
		
		public bool SplitHand();
		public void ResetHand();
	}
}
