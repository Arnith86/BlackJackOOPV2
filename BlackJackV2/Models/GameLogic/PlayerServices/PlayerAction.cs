// Project: BlackJackV2
// file: BlackJackV2/Models/GameLogic/PlayerServices/PlayerAction.cs

using Avalonia.Media.Imaging;
using BlackJackV2.Constants;
using BlackJackV2.Models.CardDeck;
using BlackJackV2.Models.CardHand;
using BlackJackV2.Models.PlayerHands;
using BlackJackV2.Models.Player;
using BlackJackV2.Services.Events;
using System.Diagnostics;
using System.Reactive.Subjects;

namespace BlackJackV2.Models.GameLogic.PlayerServices
{
	/// <summary>	
	///	Handels the blackjack related actions the players can take.  
	///	</summary>
	public class PlayerAction : IPlayerAction
	{
		/// <summary>
		/// Notifies when a split was successful
		/// </summary>
		private readonly Subject<SplitSuccessfulEvent> _splitSuccessfulEvent;

		/// <summary>
		/// Initializes a new instance of <see cref="PlayerAction"/> with a subject notyfaing when a split is performed.
		/// </summary>
		/// <param name="splitSuccessfulEvent"></param>
		public PlayerAction(Subject<SplitSuccessfulEvent> splitSuccessfulEvent)
		{
			_splitSuccessfulEvent = splitSuccessfulEvent;
		}

		// TODO: show that the player has busted
		/// Performs the Hit action by adding a card to the specified hand,
		/// provided the hand is neither busted nor folded.
		/// <inheritdoc/>
		public void PerformHit(	IBlackJackPlayerHands<Bitmap, string> playerHands, 
								IBlackJackCardHand<Bitmap, string> cardHand, 
								ICardDeck<Bitmap, string> blackJackCardDeck)
		{
			if (!cardHand.IsBusted && !cardHand.IsFolded)
				playerHands.AddCardToHand(cardHand, blackJackCardDeck.GetTopCard());
		}

		// TODO: show that the player has busted
		/// Performs the Double Down action by adding a card to the specified hand and double the chosen bet,
		/// provided the hand is neither busted nor folded, and that there are enough funds.
		/// <inheritdoc/>
		public void PerformDoubleDown(	IPlayer player,
										IBlackJackCardHand<Bitmap, string> cardHand,
										ICardDeck<Bitmap, string> blackJackCardDeck)
		{
			int bet = player.hands.GetBetFromHand(cardHand.Id);

			// If the player has enough funds, double the bet and add a card to the hand
			if (player.EnoughFundsForBet(bet) && player.hands.TryDoubleDownBet(cardHand.Id, cardHand))
			{
				player.PlaceBet(cardHand.Id, bet, true);
				player.hands.AddCardToHand(cardHand, blackJackCardDeck.GetTopCard());
				player.hands.FoldHand(cardHand);
			}
			else
			{
				Debug.WriteLine("Double down failed");
				// TODO: Add a message to the user that the double down was not successfull
			}
		}

		// TODO: Show that the player has busted
		/// provided the hand is neither busted nor folded.
		/// <inheritdoc/>
		public void PerformFold(	IBlackJackPlayerHands<Bitmap, string> playerHands, 
									IBlackJackCardHand<Bitmap, string> cardHand, 
									ICardDeck<Bitmap, string> blackJackCardDeck)
		{
			if (!cardHand.IsBusted)
				playerHands.FoldHand(cardHand);
		}

		/// Performs the Split action by moving the second card to the split hand and adding a card to each hand,
		/// provided the hand is neither busted nor folded, and there is enough funds.
		/// <inheritdoc/>
		public void PerformSplit(	IPlayer player,
									ICardDeck<Bitmap, string> blackJackCardDeck)
		{
			int primaryBet = player.hands.GetBetFromHand(HandOwners.HandOwner.Primary);

			if (player.EnoughFundsForBet(primaryBet) && 
				player.hands.TrySplitHand(out var splitHands) )
			{
				player.hands.AddCardToHand(splitHands.primary, blackJackCardDeck.GetTopCard());
				player.hands.AddCardToHand(splitHands.split, blackJackCardDeck.GetTopCard());

				// Notify that the split was successful
				_splitSuccessfulEvent.OnNext(new SplitSuccessfulEvent(player.Name));
			}
			else
			{
				// TODO : Add a message to the user that the split was not successfull
			}
		}
	}
}
