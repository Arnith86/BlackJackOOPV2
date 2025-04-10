using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackV2.Constants
{
	public static class BlackJackActions
	{
		// Possible actions in the blackjack game
		public enum PlayerActions
		{
			Hit,
			DoubleDown,
			Fold,
			Split
		}
	}
}
