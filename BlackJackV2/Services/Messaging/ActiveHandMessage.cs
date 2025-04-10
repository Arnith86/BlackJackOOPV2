using BlackJackV2.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackV2.Services.Messaging
{

	/**
	 * This class is used to send messages about the active hand in the game.
	 * The message contains the active hand, which can be either the primary, split or dealer.
	 **/
	public class ActiveHandMessage
	{
		public HandOwners.HandOwner ActiveHand { get; }

		public ActiveHandMessage(HandOwners.HandOwner activeHand)
		{
			ActiveHand = activeHand;
		}
	}
}
