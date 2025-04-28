// Project: BlackJackV2
// file: BlackJackV2/Models/GameLogic/GameCoordinator.cs



using BlackJackV2.Models.CardDeck;
using BlackJackV2.Models.CardHand;
using BlackJackV2.Models.GameLogic.CardServices;
using BlackJackV2.Models.GameLogic.Dealer_Services;
using BlackJackV2.Models.GameLogic.GameRuleServices;
using BlackJackV2.Models.GameLogic.PlayerServices;
using BlackJackV2.Models.Player;
using System.Collections.Generic;
using System.Threading.Tasks;


/// <summary>
///		This is a Facade used to simplefy game logic classes and functionality  
///		
///		public		Subject<Dictionary<string, IPlayer>>		PlayerChangedEvent		:	Subject to notify if players in game change
///		public		Subject<BetUpdateEvent>						BetUpdateEvent			:	Used to notify when the bet value is updated
///		public		Subject<IPlayer>							BetRequestedEvent		:	Subject to notify when the bet is requested
///		public		Subject<SplitSuccessfulEvent>				splitSuccessfulEvent	:	Subject to notify when the player split is successful
///		public		IObservable<GameState>						GameStateObservable		:	Subject and IObservable to notify when the game state changes
///		
///		public		IBlackJackPlayerHands<Bitmap, string>		DealerCardHand			:	Represents the dealers hands	
///  
/// 	async Task		RegisterBetForNewRound();								: Initiates and wait for player bets retrival
///		void			OnBetInputReceived(string playerName, int betInput)		: Called when the player inputs their bet	
///		async Task		StartNewRound()											: Starts a new round of the game, handles player turns and dealer's turn
///		void			EvaluateRound()											: Evaluates the round and determines the winner
///		void			OnPlayerChangedReceived(List<string> playerNames)		: Called when the current players are changed
/// </summary>


namespace BlackJackV2.Models.GameLogic
{
	public class GameCoordinator<TImage, TValue> : IGameCoordinator<TImage, TValue>
	{
		private ICardServices<TImage, TValue> _coreServices;
		private IDealerServices<TImage,TValue> _dealerServices;
		private IPlayerServices<TImage, TValue> _playerServices;
		private GameRuleServices<TImage, TValue> _gameRuleServices;

		
		public GameCoordinator(	ICardServices<TImage, TValue> coreServices,
								IDealerServices<TImage, TValue> dealerServices,
								IPlayerServices<TImage, TValue> playerServices,
								GameRuleServices<TImage, TValue> gameRuleServices	) 
		{
			_coreServices = coreServices;
			_dealerServices = dealerServices;
			_playerServices = playerServices;
			_gameRuleServices = gameRuleServices;
			
		}

		// Initiates and wait for player bets retrival
		public async Task RegisterBetForNewRound()
		{
			_playerServices.RegisterBetForNewRound();
		}


		// Starts a new round of the game, handles player turns and dealer's turn
		public async Task StartNewRound()
		{
			ICardDeck<TImage, TValue> cardDeck = _coreServices.CardDeck;
			_coreServices.CardDeck.ShuffleDeck();

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


		public void EvaluateRound()
		{
			// Evaluate the round and determine the winner
			//Debug.WriteLine(roundEvaluator.EvaluateRound(PlayerCardHand.PrimaryCardHand, DealerCardHand.PrimaryCardHand));
		}

		private void EvaluateSingleHand(BlackJackCardHand<TImage, TValue> cardHand)
		{

		}
	}
}

