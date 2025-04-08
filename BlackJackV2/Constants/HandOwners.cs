using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackV2.Constants
{
	public static class HandOwners
	{
		// Possible hands in the blackjack game
		public enum HandOwner
		{
			Primary,
			Split,
			Dealer
		}
	}
}
