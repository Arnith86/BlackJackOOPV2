using Avalonia.Media.Imaging;
using BlackJackV2.Models.CardHand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackV2.Models.Player
{
	internal class BlackJackPlayerHandCreator
	{
		public PlayerHand CreateBlackJackPlayerHand()
		{
			BlackJackCardHandCreator blackJackCardHandCreator = new BlackJackCardHandCreator();

			return new PlayerHand(blackJackCardHandCreator.CreateBlackJackCardHand());
		}
	}
}
