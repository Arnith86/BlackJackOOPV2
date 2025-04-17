// Project: BlackJackV2
// file: BlackJackV2/Models/GameLogic/GameState.cs

/// <summary>	
///
///		Handels the blackjack related actions the players can take 
///		
///		Subject<SplitSuccessfulEvent>	_splitSuccessfulEvent	: Subject to notify when a split was successful
///				
///		void		Hit()			: They receive exactly one more card
///		void		DoubleDown()	: The player doubles their original bet. Player receive exactly one more card. 
///										After that, they are forced to stand — no more cards can be drawn for that hand.
///		void		Fold()			: No more cards can be drawn for that hand.
///		void		Split()			: Only on the initial deal – The player must receive two identical rank cards (e.g., two 10s, two Kings).
///										Each split hand gets a new card – After splitting, the dealer gives one additional card to each hand.
///										Additional bets – The player must double their original bet to play both hands.
///										NOT IMPLEMENTED Restrictions on Aces – If a player splits Aces, they typically receive only one additional card per Ace and cannot hit further.
///	</summary>

using Avalonia.Media.Imaging;
using BlackJackV2.Models.CardDeck;
using BlackJackV2.Models.CardHand;
using BlackJackV2.Models.Player;
using BlackJackV2.Services.Events;
using System.Diagnostics;
using System.Reactive.Subjects;

namespace BlackJackV2.Models.GameLogic
{
	public class PlayerAction
	{
		// Subject to notify when a split was successful
		Subject<SplitSuccessfulEvent> _splitSuccessfulEvent;

		public PlayerAction(Subject<SplitSuccessfulEvent> splitSuccessfulEvent)
		{
			_splitSuccessfulEvent = splitSuccessfulEvent;
		}


		// Performes the action of hitting a card, if the player is not busted
		// TODO: show that the player has busted
		public void Hit(IPlayerHands<Bitmap, string> playerHands, 
						IBlackJackCardHand<Bitmap, string> cardHand, 
						ICardDeck<Bitmap, string> blackJackCardDeck)
		{
			if (!cardHand.IsBusted && !cardHand.IsFolded)
				playerHands.AddCardToHand(cardHand, blackJackCardDeck.GetTopCard());
		}

		// Performes the action of doubling down, if the player is not busted and not folded
		public void DoubleDown(	int playerFunds,
								IPlayerHands<Bitmap, string> playerHands,
								IBlackJackCardHand<Bitmap, string> cardHand,
								ICardDeck<Bitmap, string> blackJackCardDeck)
		{
			// If the player has enough funds, double the bet and add a card to the hand
			if (playerHands.TryDoubleDownBet(playerFunds, cardHand))
			{
				playerHands.AddCardToHand(cardHand, blackJackCardDeck.GetTopCard());
				playerHands.FoldHand(cardHand);
			}
			else
			{
				Debug.WriteLine("Double down failed");
				// TODO: Add a message to the user that the double down was not successfull
			}
		}

		// Performes the action of folding a hand, if the player is not busted
		// TODO: Show that the player has busted
		public void Fold(	IPlayerHands<Bitmap, string> playerHands, 
							IBlackJackCardHand<Bitmap, string> cardHand, 
							ICardDeck<Bitmap, string> blackJackCardDeck)
		{
			if (!cardHand.IsBusted)
				playerHands.FoldHand(cardHand);
		}

		public void Split(	string playerName,
							int playerFunds, 
							IPlayerHands<Bitmap, string> playerHands, 
							ICardDeck<Bitmap, string> blackJackCardDeck)
		{	
			if (playerHands.TrySplitHand())
			{
				playerHands.AddCardToHand(playerHands.PrimaryCardHand, blackJackCardDeck.GetTopCard());
				playerHands.AddCardToHand(playerHands.SplitCardHand, blackJackCardDeck.GetTopCard());

				// Notify that the split was successful
				_splitSuccessfulEvent.OnNext(new SplitSuccessfulEvent(playerName));
			}
			else
			{
				// TODO : Add a message to the user that the split was not successfull
			}
		}
	}
}
