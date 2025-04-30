// Project: BlackJackV2
// file: BlackJackV2/Models/GameLogic/PlayerServices/IPlayerAction.cs

using BlackJackV2.Models.CardDeck;
using BlackJackV2.Models.Player;
using BlackJackV2.Services.Events;

namespace BlackJackV2.Models.GameLogic.PlayerServices
{
	/// <summary>
	/// Interface handling possible player actions for a Blackjack player's hand.
	/// </summary>
	public interface IPlayerAction<TImage, TValue>
	{
		/// <summary>
		/// Performs the Hit action on a specific hand.
		/// </summary>
		/// <param name="playerActionEvent">Represents an event containing information about a player's action during a game round.</param>
		/// <param name="player">The player performing the action.</param>
		/// <param name="blackJackCardDeck">The deck which the card object is drawn from.</param>
		public void PerformHit( 
			PlayerActionEvent playerActionEvent,
			IPlayer<TImage, TValue> player,
			ICardDeck<TImage, TValue> blackJackCardDeck	);

		/// <summary>
		/// Performes the Double Down action on a specific hand.
		/// </summary>
		/// <param name="playerActionEvent">Represents an event containing information about a player's action during a game round.</param>
		/// <param name="player">The player performing the action.</param>
		/// <param name="blackJackCardDeck">The deck which the card object is drawn from.</param>
		public void PerformDoubleDown(	
			PlayerActionEvent playerActionEvent,
			IPlayer<TImage, TValue> player,
			ICardDeck<TImage, TValue> blackJackCardDeck);

		/// <summary>
		/// Performes the Fold action on a specific hand. 
		/// </summary>
		/// <param name="playerActionEvent">Represents an event containing information about a player's action during a game round.</param>
		/// <param name="player">The player performing the action.</param>
		/// <param name="blackJackCardDeck">The deck which the card object is drawn from.</param>
		public void PerformFold(	
			PlayerActionEvent playerActionEvent,
			IPlayer<TImage, TValue> player,
			ICardDeck<TImage, TValue> blackJackCardDeck);

		/// <summary>
		/// Performes the Split action.
		/// </summary>
		/// <param name="player">The player performing the action.</param>
		/// <param name="blackJackCardDeck">The deck which the card object is drawn from.</param>
		public void PerformSplit(IPlayer<TImage, TValue> player, ICardDeck<TImage, TValue> blackJackCardDeck);
	}
}
