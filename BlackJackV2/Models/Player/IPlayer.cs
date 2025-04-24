// Project: BlackJackV2
// file: BlackJackV2/Models/Player/IPlayer.cs

using BlackJackV2.Constants;
using BlackJackV2.Models.PlayerHands;

namespace BlackJackV2.Models.Player
{
	/// <summary>
	/// Represents a player in a Blackjack game, with a name, available funds, and one or more hands.
	/// This interface also supports placing bets, checking fund sufficiency, and receiving payouts.
	/// Serves as the product in the Player Factory pattern.
	/// </summary>
	public interface IPlayer<TImage, TValue>
	{
		/// <summary>
		/// Gets the player's name.
		/// </summary>
		public string Name { get; }

		/// <summary>
		/// Gets the current amount of funds available to the player.
		/// </summary>
		public int Funds { get; }

		/// <summary>
		/// Gets the player's hand wrapper, which includes both primary and split hands.
		/// </summary>
		public IBlackJackPlayerHands<TImage, TValue> Hands { get; }

		/// <summary>
		/// Places a bet on the specified hand.
		/// </summary>
		/// <param name="owner">The hand to place the bet on (primary or split).</param>
		/// <param name="amount">The bet amount.</param>
		/// <param name="doubleDown">Set to true if this is a double down bet.</param>
		/// <returns>True if the bet was placed successfully; otherwise, false.</returns>
		/// <remarks> Related files <see cref="BlackJackV2.Factories.PlayerFactory"/></remarks>
		public bool PlaceBet(HandOwners.HandOwner owner, int amount, bool doubleDown = false);

		/// <summary>
		/// Checks whether the player has sufficient funds for a given bet.
		/// </summary>
		/// <param name="amount">The amount to check against the player's available funds.</param>
		/// <returns>True if funds are sufficient; otherwise, false.</returns>
		public bool EnoughFundsForBet(int amount);

		/// <summary>
		/// Adds the specified amount to the player's funds (used for payouts).
		/// </summary>
		/// <param name="amount">The amount to add.</param>
		public void PayOut(int amount);
	}
}
