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

	internal interface IPlayerHand<TFrontImage, TBackImage, TValue>
	{
		public List<ICardHand<TFrontImage, TBackImage, TValue>> CardHands { get; }
		public void SplitHand(string splitValue, ICardHand<TFrontImage, TBackImage, TValue> splitHand);
		public void ResetHand();
	}
}
