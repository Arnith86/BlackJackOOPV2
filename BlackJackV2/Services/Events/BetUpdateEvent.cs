using Avalonia.Media.Imaging;
using BlackJackV2.Models.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackV2.Services.Events
{
	/**
	 * This class is responsible for handling the bet update events in the game.
	 * It contains the player hands and is used to notify when a bet has been updated.
	 **/
	public class BetUpdateEvent
	{
		public IPlayerHands<Bitmap, string> PlayerHands { get; }

		public BetUpdateEvent(IPlayerHands<Bitmap, string> playerHands)
		{
			PlayerHands = playerHands;
		}
	}
}
