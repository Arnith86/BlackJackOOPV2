// Project: BlackJackV2
// file: BlackJackV2/Models/GameLogic/PlayerServices/PlayerRound.cs

using System;
using BlackJackV2.Models.CardDeck;
using BlackJackV2.Models.CardHand;
using BlackJackV2.Models.Player;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using BlackJackV2.Services.Events;
using BlackJackV2.Shared.Constants;
using System.Reactive.Disposables;

namespace BlackJackV2.Models.GameLogic.PlayerServices
{
	/// <summary>
	/// Handles an instance of a player's turn in a Blackjack round,
	/// managing their primary and split hands, reacting to player actions, 
	/// and coordinating card draws and game state updates.
	/// </summary>
	public class PlayerRound<TImage, TValue> : IPlayerRound<TImage, TValue>
	{
		private ICardDeck<TImage, TValue> _cardDeck;
		private IPlayer<TImage, TValue> _player;
		
		// The player action class handles the blackjack related actions the players can take
		private IPlayerAction<TImage, TValue> _playerAction;

		// The current active hand in the game
		private IBlackJackCardHand<TImage, TValue> currentHand;

		// Queue of card hands to handle
		private Queue<IBlackJackCardHand<TImage, TValue>> blackJackCardHands = new Queue<IBlackJackCardHand<TImage, TValue>>();

		/// <summary>
		/// Subject used to listen for player actions during their turn.
		/// </summary>
		public Subject<BlackJackActions.PlayerActions> PlayerActionSubject { get; }

		//// Notifies when a hand has changed
		//private Subject<Unit> _roundCompletedSubject = new Subject<Unit>();
		//public IObservable<Unit> RoundCompletedObservable => _roundCompletedSubject;

		/// <summary>
		/// Manages subscriptions that need to be disposed when the round ends.
		/// </summary>
		private readonly CompositeDisposable _disposables = new CompositeDisposable();

		/// <summary>
		/// Initializes a new instance of the <see cref="PlayerRound{TImage, TValue}"/> class.
		/// </summary>
		/// <param name="playerAction">Service for performing player actions during the round.</param>
		/// <param name="playerActionSubject">Subject to listen for player actions from the UI or input system.</param>
		/// <param name="splitSuccessfulEvent">Event that notifies when a player's hand has been successfully split.</param>
		public PlayerRound(	IPlayerAction<TImage, TValue> playerAction, 
							Subject<BlackJackActions.PlayerActions> playerActionSubject,
							Subject<SplitSuccessfulEvent> splitSuccessfulEvent )
		{
			_playerAction = playerAction;
			PlayerActionSubject = playerActionSubject;
			
			splitSuccessfulEvent
			.Subscribe(splitEvent =>
			{
				// If the player split was successful, add the split hand to the player card view models
				// and update the bet values
				if (splitEvent.PlayerName == _player.Name)
				{
					blackJackCardHands.Enqueue(_player.Hands.SplitCardHand);
				}
			}).DisposeWith(_disposables);
		}

		/// <summary>
		/// Disposes of any active subscriptions related to the player's round.
		/// </summary>
		public void Dispose() => _disposables.Dispose();

		/// <summary>
		/// Handles the logic for a player's full turn, including their primary and any split hands.
		/// Waits for player actions and updates the game state accordingly.
		/// </summary>
		/// <param name="cardDeck">The deck used for drawing cards.</param>
		/// <param name="player">The player taking their turn.</param>
		public async Task PlayerTurn(ICardDeck<TImage, TValue> cardDeck, IPlayer<TImage, TValue> player) 
		{
			_cardDeck = cardDeck;
			_player = player;

			blackJackCardHands.Enqueue(_player.Hands.PrimaryCardHand);

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
					BlackJackActions.PlayerActions action = await PlayerActionSubject.FirstAsync();
					ProcessPlayerAction(action);
				}

				// Hand is finished
				currentHand.IsActive = false;
			}
		}

		//// Awaits player action input
		//private async Task<BlackJackActions.PlayerActions> WaitForPlayerAction()
		//{
		//	return await _playerActionSubject.FirstAsync();
		//}

		/// <summary>
		/// Processes the player action received during the turn and applies the corresponding game logic.
		/// </summary>
		/// <param name="action">The player action to process (e.g., Hit, Fold, DoubleDown, Split).</param>
		private void ProcessPlayerAction(BlackJackActions.PlayerActions action)
		{
			switch (action)
			{
				case BlackJackActions.PlayerActions.Hit:
					_playerAction.PerformHit(_player.Hands, currentHand, _cardDeck);
					break;
				case BlackJackActions.PlayerActions.DoubleDown:
					_playerAction.PerformDoubleDown(_player, currentHand, _cardDeck);
					break;
				case BlackJackActions.PlayerActions.Fold:
					_playerAction.PerformFold(_player.Hands, currentHand, _cardDeck);
					break;
				case BlackJackActions.PlayerActions.Split:
					_playerAction.PerformSplit(_player, _cardDeck);
					break;
				default:
					break;
			}
		}
	}
}
