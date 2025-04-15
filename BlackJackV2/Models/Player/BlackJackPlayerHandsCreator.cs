using Avalonia.Media.Imaging;
using BlackJackV2.Constants;
using BlackJackV2.Models.CardHand;
using BlackJackV2.Services.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackV2.Models.Player
{
	/**
	 * Creates an object that stores player hands for a blackjack game.
	 * Uses the BlackJackCardHandCreator to create a hand of cards.
	 **/

	internal class BlackJackPlayerHandsCreator
	{
		public static IPlayerHands<Bitmap, string> CreateBlackJackPlayerHand(ISubject<BetUpdateEvent> betUpdateSubject, HandOwners.HandOwner id)
		{
			return new PlayerHands(betUpdateSubject, id, BlackJackCardHandCreator.CreateBlackJackCardHand());
		}
	}
}
