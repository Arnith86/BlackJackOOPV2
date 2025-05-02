// Project: BlackJackV2
// file: BlackJackV2/Models/GameLogic/PlayerServices/PlayerAction.cs

using BlackJackV2.Models.CardDeck;
using BlackJackV2.Models.CardHand;
using BlackJackV2.Models.Player;
using BlackJackV2.Services.Events;
using System.Diagnostics;
using System.Reactive.Subjects;
using BlackJackV2.Models.GameLogic.GameRuleServices.Interfaces;
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

		private readonly IGameRules<TImage, TValue> _ruleServices;


		/// <summary>
		/// Initializes a new instance of <see cref="IPlayerAction{TImage, TValue}"/> with a subject to notify when a split is successful.
		/// </summary>
		/// <param name="splitSuccessfulEvent"></param>
		public PlayerAction(Subject<SplitSuccessfulEvent> splitSuccessfulEvent, IGameRules<TImage, TValue> gameRules)
		{
			_splitSuccessfulEvent = splitSuccessfulEvent;
			_ruleServices = gameRules;
		}

		private IBlackJackCardHand<TImage, TValue> GetCardHand(IPlayer<TImage, TValue> player, HandOwners.HandOwner primaryOrSplit)
		{
			return player.Hands.GetCardHand(primaryOrSplit);
		}

		// TODO: show that the player has busted
		/// Performs the Hit action by adding a card to the specified hand,
		/// provided the hand is neither busted nor folded.
		/// <inheritdoc/>
		public void PerformHit(
			PlayerActionEvent playerActionEvent,
			IPlayer<TImage, TValue> player,
			ICardDeck<TImage, TValue> blackJackCardDeck)
		{
			IBlackJackCardHand<TImage, TValue> cardHand = GetCardHand(player, playerActionEvent.PrimaryOrSplit);

			if (cardHand is {IsBusted: false, IsFolded: false})
				player.Hands.AddCardToHand(cardHand, blackJackCardDeck.GetTopCard());
		}

		// TODO: show that the player has busted
		/// Performs the Double Down action by adding a card to the specified hand and double the chosen bet,
		/// provided the hand is neither busted nor folded, has exactly two cards, and that there are enough funds.
		/// <inheritdoc/>
		public void PerformDoubleDown(	
			PlayerActionEvent playerActionEvent,
			IPlayer<TImage, TValue> player,
			ICardDeck<TImage, TValue> blackJackCardDeck)
		{
			IBlackJackCardHand<TImage, TValue> cardHand = GetCardHand(player, playerActionEvent.PrimaryOrSplit);

			var result = _ruleServices.CanDoubleDown(player, cardHand.Id);
			
			if (!result.IsAllowed)
			{
				//TODO: Send message to the user that the double down was not successful. 
				Debug.WriteLine(result.Message);
			}
			else
			{
				int bet = player.Hands.GetBetFromHand(cardHand.Id);

				player.PlaceBet(cardHand.Id, bet, true);
				
				player.Hands.AddCardToHand(cardHand, blackJackCardDeck.GetTopCard());
				player.Hands.FoldHand(cardHand);
			}
		}

		// TODO: Show that the player has busted
		/// provided the hand is neither busted nor folded.
		/// <inheritdoc/>
		public void PerformFold(
			PlayerActionEvent playerActionEvent,
			IPlayer<TImage, TValue> player,
			ICardDeck<TImage, TValue> blackJackCardDeck)
		{
			IBlackJackCardHand<TImage, TValue> cardHand = GetCardHand(player, playerActionEvent.PrimaryOrSplit);

			var result = _ruleServices.CanFold(player, cardHand.Id);

			if (!result.IsAllowed)
			{
				Debug.WriteLine(result.Message);
			}
			else
			{
				if (cardHand is { IsBusted: false })
					player.Hands.FoldHand(cardHand);
			}
		}

		/// Performs the Split action by moving the second card to the split hand and adding a card to each hand,
		/// provided the hand is neither busted nor folded, and there is enough funds.
		/// <inheritdoc/>
		public void PerformSplit(IPlayer<TImage, TValue> player, ICardDeck<TImage, TValue> blackJackCardDeck)
		{
			var result = _ruleServices.CanSplit(player);

			if(!result.IsAllowed)
			{
				Debug.WriteLine(result.Message);
				// TODO : Send message to the user that the split was not successfull
			}
			else
			{
				var (primary, split) = player.Hands.SplitHand();
				player.Hands.AddCardToHand(primary, blackJackCardDeck.GetTopCard());
				player.Hands.AddCardToHand(split, blackJackCardDeck.GetTopCard());

				// Notify that the split was successful
				_splitSuccessfulEvent.OnNext(new SplitSuccessfulEvent(player.Name));
			}
		}
	}
}
