// Project: BlackJackV2
// file: BlackJackV2/Factories/PlayerFactory/PlayerCreator.cs

/// <summary>
///		Part of PlayerFactory pattern
///		Concreate creator for Player object creation.
/// </summary>
/// 

using Avalonia.Media.Imaging;
using BlackJackV2.Models.Player;
using BlackJackV2.Models.PlayerHands;
using BlackJackV2.Services.Events;
using System.Reactive.Subjects;

namespace BlackJackV2.Factories.PlayerFactory
{
	public class PlayerCreator : IPlayerCreator<Bitmap, string>
	{
		public override IPlayer CreatePlayer(IBlackJackPlayerHands<Bitmap, string> playerHands, ISubject<BetUpdateEvent> betUpdatedSubject, string name = "Player1")
		{
			return new Player(name, playerHands, betUpdatedSubject);
		}
	}
}
