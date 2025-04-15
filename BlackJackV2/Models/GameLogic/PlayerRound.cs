using Avalonia.Controls.Documents;
using Avalonia.Controls.Shapes;
using BlackJackV2.Constants;
using BlackJackV2.Models.CardDeck;
using BlackJackV2.Models.CardHand;
using BlackJackV2.Models.Player;
using BlackJackV2.Constants;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using BlackJackV2.Services.Messaging;

namespace BlackJackV2.Models.GameLogic
{
	public class PlayerRound
	{
		private BlackJackCardDeck _cardDeck;
		
		// The current active player 
		private PlayerHands _playerHands;

		// The player action class handles the blackjack related actions the players can take
		private PlayerAction _playerAction;

		// The current active hand in the game
		private BlackJackCardHand currentHand;

		// Queue of card hands to handle
		private Queue<BlackJackCardHand> blackJackCardHands = new Queue<BlackJackCardHand>();

		// Regesters player actions events
		public Subject<BlackJackActions.PlayerActions> _playerActionSubject = new Subject<BlackJackActions.PlayerActions>();

		//// Notifies when a hand has changed
		//private Subject<Unit> _roundCompletedSubject = new Subject<Unit>();
		//public IObservable<Unit> RoundCompletedObservable => _roundCompletedSubject;

		public PlayerRound(BlackJackCardDeck cardDeck, PlayerAction playerAction)
		{
			_cardDeck = cardDeck;
			_playerAction = playerAction;
			_playerHands = null;

			// If a split was registered then add the split hand to the queue
			MessageBus.Current.Listen<SplitSuccessfulMessage>().Subscribe( _ =>
			{
				blackJackCardHands.Enqueue((BlackJackCardHand)_playerHands.SplitCardHand);
			});
		}

		// This method is called when the player is taking their turn
		public async Task PlayerTurn(PlayerHands playerHands) 
		{
			_playerHands = playerHands;

			blackJackCardHands.Enqueue((BlackJackCardHand)_playerHands.PrimaryCardHand);

			// If a split was registered through the MessageBuss, then it is added to the queue of hands to handle
			while (blackJackCardHands.Count > 0)
			{
				currentHand = blackJackCardHands.Dequeue();

				// Notify that the current hand is active
				MessageBus.Current.SendMessage(new ActiveHandMessage(currentHand.Id));

				// Each iteration represents a hand in unfinished state 
				while (!currentHand.IsBusted && !currentHand.IsFolded && !currentHand.IsBlackJack)
				{
					// Pause here and wait until a player does something (Hit, Fold, etc ), and once they do, continue with the loop or logic.
					BlackJackActions.PlayerActions action = await _playerActionSubject.FirstAsync();
					ProcessPlayerAction(action);
				}
			}
		}

		// This method is called when player has takes an action
		private void ProcessPlayerAction(BlackJackActions.PlayerActions action)
		{
			switch (action)
			{
				case BlackJackActions.PlayerActions.Hit:
					_playerAction.Hit(currentHand, _cardDeck);
					break;
				case BlackJackActions.PlayerActions.DoubleDown:
					_playerAction.DoubleDown(_playerHands);       // Finish this, does nothing yet  <--------
					break;
				case BlackJackActions.PlayerActions.Fold:
					_playerAction.Fold(currentHand, _cardDeck);
					break;
				case BlackJackActions.PlayerActions.Split:
					_playerAction.Split(_playerHands, _cardDeck);
					break;
				default:
					break;
			}
		}
	}
}
