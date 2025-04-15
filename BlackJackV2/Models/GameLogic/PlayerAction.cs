using Avalonia.Media.Imaging;
using BlackJackV2.Models.CardDeck;
using BlackJackV2.Models.CardHand;
using BlackJackV2.Models.Player;
using BlackJackV2.Services.Messaging;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BlackJackV2.Models.GameLogic
{
	/**
	 * Handels the blackjack related actions the players can take 
	 **/
	
	public class PlayerAction
	{
		// Performes the action of hitting a card, if the player is not busted
		// TODO: show that the player has busted
		public void Hit(IBlackJackCardHand<Bitmap, string> cardHand, BlackJackCardDeck blackJackCardDeck)
		{
			if (!cardHand.IsBusted && !cardHand.IsFolded)
			{
				cardHand.AddCard(blackJackCardDeck.GetTopCard());
			}
		}

		public void Fold(IBlackJackCardHand<Bitmap, string> cardHand, BlackJackCardDeck blackJackCardDeck)
		{
			 cardHand.IsFolded = true;
		}

		public void DoubleDown(IPlayerHands<Bitmap, string> player)
		{

		}
		/**
		 * Only on the initial deal – The player must receive two identical rank cards (e.g., two 10s, two Kings).
		 * Each split hand gets a new card – After splitting, the dealer gives one additional card to each hand.
		 * Additional bets – The player must double their original bet to play both hands.
		 * Resplitting – Some casinos allow resplitting if another pair appears (e.g., drawing another 8 after splitting 8s).
		 * Restrictions on Aces – If a player splits Aces, they typically receive only one additional card per Ace and cannot hit further.
		 **/
			public void Split(IPlayerHands<Bitmap, string> playerHand, BlackJackCardDeck blackJackCardDeck)
		{	
			if (playerHand.SplitHand())
			{
				playerHand.PrimaryCardHand.AddCard(blackJackCardDeck.GetTopCard());
				playerHand.SplitCardHand.AddCard(blackJackCardDeck.GetTopCard());
				// Notify that the split was successful
				MessageBus.Current.SendMessage(new SplitSuccessfulMessage(true, playerHand));
			}
		}
	}
}
