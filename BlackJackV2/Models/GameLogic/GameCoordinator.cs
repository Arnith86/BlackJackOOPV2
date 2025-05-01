// Project: BlackJackV2
// file: BlackJackV2/Models/GameLogic/GameCoordinator.cs

using BlackJackV2.Factories.PlayerFactory;
using BlackJackV2.Models.CardDeck;
using BlackJackV2.Models.CardHand;
using BlackJackV2.Models.GameLogic.CardServices;
using BlackJackV2.Models.GameLogic.CoreServices;
using BlackJackV2.Models.GameLogic.Dealer_Services;
using BlackJackV2.Models.GameLogic.GameRuleServices;
using BlackJackV2.Models.GameLogic.PlayerServices;
using BlackJackV2.Models.Player;
using BlackJackV2.Services.Events;
using BlackJackV2.Shared.Constants;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace BlackJackV2.Models.GameLogic
{
	/// <summary>
	/// Coordinates the full lifecycle of a Blackjack game round,
	/// including betting, card dealing, player turns, dealer actions, and round evaluation.
	/// </summary>
	public class GameCoordinator<TImage, TValue> : IGameCoordinator<TImage, TValue>
	{
		private ICardServices<TImage, TValue> _cardServices;
		private IDealerServices<TImage,TValue> _dealerServices;
		private IPlayerServices<TImage, TValue> _playerServices;
		private GameRuleServices<TImage, TValue> _gameRuleServices;

		/// <summary>
		/// Constructs a new instance of the <see cref="GameCoordinator{TImage, TValue}"/> class
		/// with required services for core card logic, dealer logic, player logic, and game rules.
		/// </summary>
		public GameCoordinator(	ICardServices<TImage, TValue> cardServices,
								IDealerServices<TImage, TValue> dealerServices,
								IPlayerServices<TImage, TValue> playerServices,
								GameRuleServices<TImage, TValue> gameRuleServices   ) 
		{
			_cardServices = cardServices;
			_dealerServices = dealerServices;
			_playerServices = playerServices;
			_gameRuleServices = gameRuleServices;
		}

		/// <summary>
		/// Prepares the game for a new round by prompting all players to register their bets.
		/// </summary>
		public async Task RegisterBetForNewRound()
		{
			_playerServices.RegisterBetForNewRound();
		}

		/// <summary>
		/// Starts a new round by shuffling the deck, dealing cards to the dealer and players,
		/// executing each player's turn, and concluding with the dealer's turn.
		/// </summary>
		public async Task StartNewRound()
		{
			ICardDeck<TImage, TValue> cardDeck = _cardServices.CardDeck;
			_cardServices.CardDeck.ShuffleDeck();

			// Gives dealer his initial cards
			_dealerServices.InitialDeal(_dealerServices.DealerCardHand, cardDeck);

			foreach (KeyValuePair<string, IPlayer<TImage, TValue>> player in _playerServices.Players)
			{
				// Player conducts their turn
				await _playerServices.PlayerRound.PlayerTurn(cardDeck, player.Value);
			}

			// Dealer finishes his turn
			_dealerServices.DealerFinishTurn(_dealerServices.DealerCardHand, cardDeck);
			EvaluateRound();

		}

		/// <summary>
		/// Evaluates the outcome of the round for all player hands against the dealer.
		/// </summary>
		public void EvaluateRound()
		{
			// Evaluate the round and determine the winner
			//Debug.WriteLine(roundEvaluator.EvaluateRound(PlayerCardHand.PrimaryCardHand, DealerCardHand.PrimaryCardHand));
		}

		/// <summary>
		/// Evaluates the outcome of a single hand against the dealer. (Currently unused)
		/// </summary>
		/// <param name="cardHand">The player's hand to evaluate.</param>
		private void EvaluateSingleHand(BlackJackCardHand<TImage, TValue> cardHand)
		{

		}
	}
}

