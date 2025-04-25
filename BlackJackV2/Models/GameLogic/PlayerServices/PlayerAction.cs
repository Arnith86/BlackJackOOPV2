// Project: BlackJackV2
// file: BlackJackV2/Models/GameLogic/PlayerServices/PlayerAction.cs

using BlackJackV2.Models.CardDeck;
using BlackJackV2.Models.CardHand;
using BlackJackV2.Models.PlayerHands;
using BlackJackV2.Models.Player;
using BlackJackV2.Services.Events;
using System.Diagnostics;
using System.Reactive.Subjects;
using BlackJackV2.Shared.Constants;

namespace BlackJackV2.Models.GameLogic.PlayerServices
{
	/// <summary>	
	///	Handels the blackjack related actions the players can take.  
	///	</summary>
	public class PlayerAction<TImage, TValue> : IPlayerAction<TImage, TValue>
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
		public void PerformHit(	IBlackJackPlayerHands<TImage, TValue> playerHands, 
								IBlackJackCardHand<TImage, TValue> cardHand, 
								ICardDeck<TImage, TValue> blackJackCardDeck)
		{
			if (!cardHand.IsBusted && !cardHand.IsFolded)
				playerHands.AddCardToHand(cardHand, blackJackCardDeck.GetTopCard());
		}

		// TODO: show that the player has busted
		/// Performs the Double Down action by adding a card to the specified hand and double the chosen bet,
		/// provided the hand is neither busted nor folded, and that there are enough funds.
		/// <inheritdoc/>
		public void PerformDoubleDown(	IPlayer<TImage, TValue> player,
										IBlackJackCardHand<TImage, TValue> cardHand,
										ICardDeck<TImage, TValue> blackJackCardDeck)
		{
			int bet = player.Hands.GetBetFromHand(cardHand.Id);

			// If the player has enough funds, double the bet and add a card to the hand
			if (player.EnoughFundsForBet(bet) && player.Hands.TryDoubleDownBet(cardHand))
			{
				player.PlaceBet(cardHand.Id, bet, true);
				player.Hands.AddCardToHand(cardHand, blackJackCardDeck.GetTopCard());
				player.Hands.FoldHand(cardHand);
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
		public void PerformFold(	IBlackJackPlayerHands<TImage, TValue> playerHands, 
									IBlackJackCardHand<TImage, TValue> cardHand, 
									ICardDeck<TImage, TValue> blackJackCardDeck)
		{
			if (!cardHand.IsBusted)
				playerHands.FoldHand(cardHand);
		}

		/// Performs the Split action by moving the second card to the split hand and adding a card to each hand,
		/// provided the hand is neither busted nor folded, and there is enough funds.
		/// <inheritdoc/>
		public void PerformSplit(	IPlayer<TImage, TValue> player,
									ICardDeck<TImage, TValue> blackJackCardDeck)
		{
			int primaryBet = player.Hands.GetBetFromHand(HandOwners.HandOwner.Primary);

			if (player.EnoughFundsForBet(primaryBet) && 
				player.Hands.TrySplitHand(out var splitHands) )
			{
				player.Hands.AddCardToHand(splitHands.primary, blackJackCardDeck.GetTopCard());
				player.Hands.AddCardToHand(splitHands.split, blackJackCardDeck.GetTopCard());

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
