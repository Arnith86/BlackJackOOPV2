// Project: BlackJackV2
// File: Models/Player/PlayerNameEntry.cs

using ReactiveUI;

namespace BlackJackV2.Models.Player
{
	public class PlayerNameEntry : ReactiveObject
	{
		public int PlayerIndex { get; }

		private string _playerName;

		public PlayerNameEntry(int index, string name)
		{
			PlayerIndex = index;
			_playerName = name; 
		}

		public string PlayerName
		{
			get => _playerName;
			set => this.RaiseAndSetIfChanged(ref _playerName, value);
		}
	}
}
