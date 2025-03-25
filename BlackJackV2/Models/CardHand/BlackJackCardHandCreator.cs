using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackV2.Models.CardHand
{
	/**
	 *  Returns a new BlackJackCardHand object
	 **/

	internal class BlackJackCardHandCreator
	{
		public BlackJackCardHand CreateBlackJackCardHand()
		{
			return new BlackJackCardHand();
		}
	}
}
