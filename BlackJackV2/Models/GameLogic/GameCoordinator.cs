// Project: BlackJackV2
// file: BlackJackV2/Models/GameLogic/GameCoordinator.cs

using BlackJackV2.Models.CardDeck;
using BlackJackV2.Models.CardHand;
using BlackJackV2.Models.GameLogic.CardServices;
using BlackJackV2.Models.GameLogic.Dealer_Services;
using BlackJackV2.Models.GameLogic.GameRuleServices;
using BlackJackV2.Models.GameLogic.PlayerServices;
using BlackJackV2.Models.Player;
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

		private bool _isGameOver = false;
		
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

		/// <inheritdoc/>
		public bool IsGameOver
		{
			get  
			{
				foreach (var player in _playerServices.Players)
				{
					if (player.Value.Funds > 0)
						return false;
				}
				return true;
			} 
		}

		/// <summary>
		/// Prepares the game for a new round by prompting all players to register their bets.
		/// </summary>
		public async Task RegisterBetForNewRound()
		{
			await _playerServices.RegisterBetForNewRound();
		}

		public void ResetCardHands()
		{
			_dealerServices.ResetDealerCardHand();
			_playerServices.ResetPlayerCardHands();
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
		}

		/// <summary>
		/// Evaluates the outcome of the round for all player hands against the dealer.
		/// </summary>
		/// <returns>Task representing the asynchronous operation.</returns>
		public Task EvaluateRound()
		{
			foreach (var (playerName, currentPlayer) in _playerServices.Players) 
			{
				IBlackJackCardHand<TImage, TValue> splitHand = currentPlayer.Hands.GetCardHand(HandOwners.HandOwner.Split);

				EvaluateSingleHand(currentPlayer, HandOwners.HandOwner.Primary);

				if (splitHand.Hand.Count > 0)
					EvaluateSingleHand(currentPlayer, HandOwners.HandOwner.Split);
			}
			return Task.CompletedTask; 
		}

		/// <summary>
		/// Evaluates the outcome of a single hand against the dealer. (Currently unused)
		/// </summary>
		/// <param name="cardHand">The player's hand to evaluate.</param>
		private void EvaluateSingleHand(IPlayer<TImage, TValue> player, HandOwners.HandOwner primaryOrSplit)
		{
			IBlackJackCardHand<TImage, TValue> cardHand = player.Hands.GetCardHand(primaryOrSplit);

			int bet = player.Hands.GetBetFromHand(primaryOrSplit);

			BlackJackRoundResult.RoundResult result = 
				_gameRuleServices.RoundEvaluator.EvaluateRound(cardHand, _dealerServices.DealerCardHand.PrimaryCardHand);
			
			PayOutWinnings(player, bet, result);
		}

		/// <summary>
		/// Calculates and pays out winnings based on the round result.
		/// </summary>
		/// <param name="player">The player to pay out winnings to.</param>
		/// <param name="bet">The amount the player bet.</param>
		/// <param name="result">The result of the round.</param>
		private void PayOutWinnings(IPlayer<TImage, TValue> player, int bet, BlackJackRoundResult.RoundResult result)
		{
			if (result == BlackJackRoundResult.RoundResult.PlayerWinsBlackJack)
				player.PayOut(bet * (3 / 2));
			else if (result == BlackJackRoundResult.RoundResult.PlayerWins)
				player.PayOut(bet * 2);
		}
	}
}

