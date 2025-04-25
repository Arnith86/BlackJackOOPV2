// Project: BlackJackV2
// file: BlackJackV2/Models/GameLogic/GameLogic.cs


/// <summary>
///		TODO: write a summery for this class
///		
///		Subject<Dictionary<string, IPlayer>>			PlayerChangedEvent	:	Used to notify when the players in the game change 
///		Subject<BetUpdateEvent>							BetUpdateEvent		:	Used to notify when the bet value is updated
///		Subject<IPlayer>								BetRequestedEvent	:	Subject to notify when the bet is requested
///		Subject<SplitSuccessfulEvent>					splitSuccessfulEvent:	Subject to notify when the player split is successful
///		BehaviorSubject<GameState>						_gameStateSubject	: Subject and IObservable to notify when the game state changes
///		IObservable<GameState>							GameStateObservable
///		Dictionary<string, TaskCompletionSource<int>>	_betInputTask		: Used to wait for specific player bet input to be received
///		
///		ICardDeck<>			_cardDeck; 			: Card deck used in the game.
///		PlayerAction		playerAction		: Handles the blackjack related actions the players can take.
///		DealerLogic			dealerLogic			: Handles the dealer's turn in a blackjack game.
///		RoundEvaluator		roundEvaluator		: Handles the evaluation of the round.
///		PlayerRound			playerRound			: Handles all rounds related to a players hands.
///		
///		Dictionary<string, IPlayer>		Players			: A collection of players in the game
///		IPlayerHands<Bitmap, string>	_dealerCardHand	: Represents the dealer hands.
///		IPlayerHands<Bitmap, string>	 DealerCardHand
///
///		void		UpdateGameState(Action<GameState> updateAction)		: Updates the game states and notifies subscribers
///		async void	InitiateNewRound()									: Initiates a new round of the game and wait for player bets
///		async void	StartNewRound()										: Starts a new round of the game, handles player turns and dealer's turn
///		void		EvaluateRound()										: Evaluates the round and determines the winner
///		void		EvaluateSingleHand(BlackJackCardHand cardHand)		: Evaluates a single hand
///		void		OnBetInputReceived(string playerName, int betInput)	: Called when the player inputs their bet
///		void		OnPlayerChangedReceived(List<string> playerNames)   : Called when the current players are changed
/// 
/// </summary>
namespace BlackJackV2.Models.GameLogic
{
	public class GameLogic<TImage, TValue>
	{
		private readonly IGameCoordinator<TImage, TValue> _gameCoordinator;

		public GameLogic( IGameCoordinator<TImage, TValue> gameCoordinator )
		{
			_gameCoordinator = gameCoordinator;
		}

		public void RunGameLoop() 
		{
			_gameCoordinator.RegisterBetForNewRound();
			_gameCoordinator.StartNewRound();
			_gameCoordinator.EvaluateRound();
		}
	}
}
