// Project: BlackJackV2
// file: BlackJackV2/Models/GameLogic/PlayerServices/IPlayerRound.cs

using BlackJackV2.Models.CardDeck;
using BlackJackV2.Models.Player;
using BlackJackV2.Shared.Constants;
using System.Reactive.Subjects;
using System.Threading.Tasks;

namespace BlackJackV2.Models.GameLogic.PlayerServices
{
	/// <summary>
	/// Represents the actions and behavior of a player's round in the Blackjack game.
	/// This interface defines the methods and events necessary to manage the player's turn, 
	/// including handling player actions such as Hit, Fold, Split, and Double Down.
	/// </summary>
	public interface IPlayerRound <TImage, TValue>
	{
		/// <summary>
		/// A subject that emits the player's actions during their turn in the round (e.g., Hit, Fold, Split).
		/// </summary>
		public Subject<BlackJackActions.PlayerActions> PlayerActionSubject { get; }

		/// <summary>
		/// Starts the player's turn, handling actions such as Hit, Fold, etc., for the specified player.
		/// This method will process the player's actions until their turn is complete.
		/// </summary>
		/// <param name="cardDeck">The deck of cards used to provide cards for the player.</param>
		/// <param name="player">The player whose turn is being handled.</param>
		/// <returns>A task representing the asynchronous operation of handling the player's turn.</returns>
		public Task PlayerTurn(ICardDeck<TImage, TValue> cardDeck, IPlayer<TImage, TValue> player);
	}
}
