// Project: BlackJackV2
// File: Models/Player/PlayerNameEntry.cs

using BlackJackV2.Interfaces;
using ReactiveUI;

namespace BlackJackV2.Models.Player
{
	/// <summary>
	/// Represents a player's name entry with a unique index and a name that supports data binding.
	/// </summary>
	public class PlayerNameEntry : ReactiveObject, IPlayerNameEntry
	{
		/// <summary>
		/// Gets the index of the player (used to identify and order players).
		/// </summary>
		public int PlayerIndex { get; }

		private string _playerName;

		/// <summary>
		/// Initializes a new instance of the <see cref="PlayerNameEntry"/> class.
		/// </summary>
		/// <param name="index">The unique index of the player.</param>
		/// <param name="name">The player's display name.</param>
		public PlayerNameEntry(int index, string name)
		{
			PlayerIndex = index;
			_playerName = name; 
		}

		/// <summary>
		/// Gets or sets the player's name. Notifies the UI when changed.
		/// </summary>
		public string PlayerName
		{
			get => _playerName;
			set => this.RaiseAndSetIfChanged(ref _playerName, value);
		}
	}
}
