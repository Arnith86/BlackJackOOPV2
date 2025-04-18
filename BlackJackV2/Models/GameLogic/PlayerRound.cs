﻿// Project: BlackJackV2
// file: BlackJackV2/Models/GameLogic/PlayerRound.cs

/// <summary>	
/// 	This class handles an instance of a players turn in black jack 
/// 	
///		ICardDeck<Bitmap, string>				_cardDeck				: The card deck used in this round.
///		IPlayer									_player					: The player currently taking their turn.
///		PlayerAction							_playerAction			: The player action class handles the blackjack related actions the players can take.
///		IBlackJackCardHand<Bitmap, string>		currentHand				: The current active hand in the game.
///		Queue<BlackJackCardHand>				blackJackCardHands		: Queue of card hands to handle.
///		Subject<BlackJackActions.PlayerActions> _playerActionSubject	: Regesters player actions events. 
///		Subject<SplitSuccessfulEvent>			_splitSuccessfulEvent	: Notifies when a split of the hand has happend.
///		
///		async Task		PlayerTurn(ICardDeck cardDeck, IPlayer player)				: This method is called when the player is taking their turn
///		void			ProcessPlayerAction(BlackJackActions.PlayerActions action)	: This method is called when player has takes an action
///		
///	</summary>

using BlackJackV2.Constants;
using BlackJackV2.Models.CardDeck;
using BlackJackV2.Models.CardHand;
using BlackJackV2.Models.Player;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using BlackJackV2.Services.Events;

namespace BlackJackV2.Models.GameLogic
{
	public class PlayerRound
	{
		private ICardDeck<Bitmap, string> _cardDeck;

		private IPlayer _player;
		
		// The player action class handles the blackjack related actions the players can take
		private PlayerAction _playerAction;

		// The current active hand in the game
		private IBlackJackCardHand<Bitmap, string> currentHand;

		// Queue of card hands to handle
		private Queue<BlackJackCardHand> blackJackCardHands = new Queue<BlackJackCardHand>();

		// Regesters player actions events
		public Subject<BlackJackActions.PlayerActions> _playerActionSubject = new Subject<BlackJackActions.PlayerActions>();
		
		//// Notifies when a hand has changed
		//private Subject<Unit> _roundCompletedSubject = new Subject<Unit>();
		//public IObservable<Unit> RoundCompletedObservable => _roundCompletedSubject;

		private Subject<SplitSuccessfulEvent> _splitSuccessfulEvent;
		

		public PlayerRound(	PlayerAction playerAction, 
							Subject<SplitSuccessfulEvent> splitSuccessfulEvent)
		{
			_playerAction = playerAction;
			_splitSuccessfulEvent = splitSuccessfulEvent;
		}

		// This method is called when the player is taking their turn
		public async Task PlayerTurn(ICardDeck<Bitmap, string> cardDeck, IPlayer player) 
		{
			_cardDeck = cardDeck;
			_player = player;

			blackJackCardHands.Enqueue(_player.hands.PrimaryCardHand);

			// If a split was registered through the MessageBuss, then it is added to the queue of hands to handle
			while (blackJackCardHands.Count > 0)
			{
				currentHand = blackJackCardHands.Dequeue();

				// Set the current hand to the one that is being played
				currentHand.IsActive = true;

				// Each iteration represents a hand in unfinished state 
				while (!currentHand.IsBusted && !currentHand.IsFolded && !currentHand.IsBlackJack)
				{
					// Pause here and wait until a player does something (Hit, Fold, etc ), and once they do, continue with the loop or logic.
					BlackJackActions.PlayerActions action = await _playerActionSubject.FirstAsync();
					ProcessPlayerAction(action);
				}

				// Hand is finished
				currentHand.IsActive = false;
			}
		}

		// This method is called when player has takes an action
		private void ProcessPlayerAction(BlackJackActions.PlayerActions action)
		{
			switch (action)
			{
				case BlackJackActions.PlayerActions.Hit:
					_playerAction.Hit(_player.hands, currentHand, _cardDeck);
					break;
				case BlackJackActions.PlayerActions.DoubleDown:
					_playerAction.DoubleDown(_player.Funds, _player.hands, currentHand, _cardDeck);
					break;
				case BlackJackActions.PlayerActions.Fold:
					_playerAction.Fold(_player.hands, currentHand, _cardDeck);
					break;
				case BlackJackActions.PlayerActions.Split:
					_playerAction.Split(_player.Name, _player.Funds, _player.hands, _cardDeck);
					break;
				default:
					break;
			}
		}
	}
}
