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
	 * 
	 * The player can take the following actions: 
	 * 
	 * Hit			: They receive exactly one more card
	 * 
	 * Fold			: No more cards can be drawn for that hand.
	 * 
	 * DoubleDown	: The player doubles their original bet. Player receive exactly one more card. 
	 *					After that, they are forced to stand — no more cards can be drawn for that hand.
	 * 
	 * Split		: Only on the initial deal – The player must receive two identical rank cards (e.g., two 10s, two Kings).
	 *					Each split hand gets a new card – After splitting, the dealer gives one additional card to each hand.
	 *					Additional bets – The player must double their original bet to play both hands.
	 *					* NOT IMPLEMENTED Restrictions on Aces – If a player splits Aces, they typically receive only one additional card per Ace and cannot hit further.
	 **/

	public class PlayerAction
	{
		// Performes the action of hitting a card, if the player is not busted
		// TODO: show that the player has busted
		public void Hit(IPlayerHands<Bitmap, string> playerHands, IBlackJackCardHand<Bitmap, string> cardHand, BlackJackCardDeck blackJackCardDeck)
		{
			if (!cardHand.IsBusted && !cardHand.IsFolded)
				playerHands.AddCardToHand(cardHand, blackJackCardDeck.GetTopCard());
		}

		// Performes the action of doubling down, if the player is not busted and not folded
		public void DoubleDown(IPlayerHands<Bitmap, string> playerHands,
								IBlackJackCardHand<Bitmap, string> cardHand,
								BlackJackCardDeck blackJackCardDeck)
		{
			if (playerHands.TryDoubleDownBet(cardHand))
			{
				playerHands.AddCardToHand(cardHand, blackJackCardDeck.GetTopCard());
				playerHands.FoldHand(cardHand);
			}
		}

		// Performes the action of folding a hand, if the player is not busted
		// TODO: Show that the player has busted
		public void Fold(	IPlayerHands<Bitmap, string> playerHands, 
							IBlackJackCardHand<Bitmap, string> cardHand, 
							BlackJackCardDeck blackJackCardDeck)
		{
			if (!cardHand.IsBusted)
				playerHands.FoldHand(cardHand);
		}

		public void Split(IPlayerHands<Bitmap, string> playerHands, BlackJackCardDeck blackJackCardDeck)
		{	
			if (playerHands.TrySplitHand())
			{
				playerHands.AddCardToHand(playerHands.PrimaryCardHand, blackJackCardDeck.GetTopCard());
				playerHands.AddCardToHand(playerHands.SplitCardHand, blackJackCardDeck.GetTopCard());

				// Notify that the split was successful
				MessageBus.Current.SendMessage(new SplitSuccessfulMessage(true, playerHands));
			}
		}
	}
}
