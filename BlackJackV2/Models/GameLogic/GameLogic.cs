// Project: BlackJackV2
// file: BlackJackV2/Models/GameLogic/GameLogic.cs


using System.Threading.Tasks;

namespace BlackJackV2.Models.GameLogic
{
	/// <summary>
	/// Core controller class responsible for running the Blackjack game loop.
	/// </summary>
	/// <typeparam name="TImage">The type used to represent card images.</typeparam>
	/// <typeparam name="TValue">The type used to represent card values.</typeparam>
	/// <remarks>
	/// Delegates responsibilities to the <see cref="IGameCoordinator{TImage, TValue}"/> implementation.
	/// This class ensures that each round follows the correct sequence of actions: 
	/// bet registration, round initialization, and evaluation.
	/// </remarks>
	public class GameLogic<TImage, TValue>
	{
		private readonly IGameCoordinator<TImage, TValue> _gameCoordinator;

		/// <summary>
		/// Initializes a new instance of the <see cref="GameLogic{TImage, TValue}"/> class.
		/// </summary>
		/// <param name="gameCoordinator">An instance of a class implementing <see cref="IGameCoordinator{TImage, TValue}"/> to manage game flow.</param>
		public GameLogic( IGameCoordinator<TImage, TValue> gameCoordinator )
		{
			_gameCoordinator = gameCoordinator;
		}

		/// <summary>
		/// Starts and maintains the core game loop until the game is over.
		/// </summary>
		/// <remarks>
		/// Each iteration of the loop represents one full round of Blackjack.
		/// </remarks>
		public async Task RunGameLoop() 
		{
			while (!_gameCoordinator.IsGameOver)
			{
				_gameCoordinator.ResetCardHands();

				await _gameCoordinator.RegisterBetForNewRound();
				
				// TODO: Optional: Wait for UI to indicate "Ready for dealing" if needed

				await _gameCoordinator.StartNewRound();

				// TODO: Optional: Wait for user to click "Next Round" if needed

				_gameCoordinator.EvaluateRound();

				await Task.Delay(5000); // Delay to allow time for evaluation and UI updates.


			}
		}
	}
}
