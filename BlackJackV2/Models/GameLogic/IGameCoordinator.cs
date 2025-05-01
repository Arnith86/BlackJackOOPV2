// Project: BlackJackV2
// file: BlackJackV2/Models/GameLogic/IGameCoordinator.cs

using System.Threading.Tasks;

namespace BlackJackV2.Models.GameLogic
{
	/// <summary>
	/// Coordinates the main flow of a Blackjack game round, including handling bets,
	/// starting rounds, and evaluating results.
	/// </summary>
	public interface IGameCoordinator<TImage, TValue>
	{
		/// <summary>
		/// Prompts the player(s) to place their bets and prepares the system for a new round.
		/// </summary>
		public Task RegisterBetForNewRound();
		
		/// <summary>
		/// Executes the sequence for a new round: deals initial cards, processes all player turns,
		/// and performs the dealer's turn.
		/// </summary>
		public Task StartNewRound();

		/// <summary>
		/// Evaluates all hands at the end of the round and determines the outcome for each player.
		/// </summary>
		public void EvaluateRound();
	}
}
