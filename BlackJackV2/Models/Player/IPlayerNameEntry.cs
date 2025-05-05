// Project: BlackJackV2
// File: Models/Player/IPlayerNameEntry.cs

namespace BlackJackV2.Interfaces
{
	/// <summary>
	/// Represents a contract for accessing and modifying a player's name entry.
	/// </summary>
	public interface IPlayerNameEntry
	{
		/// <summary>
		/// Gets the unique index of the player.
		/// </summary>
		int PlayerIndex { get; }

		/// <summary>
		/// Gets or sets the player's name.
		/// </summary>
		string PlayerName { get; set; }
	}
}