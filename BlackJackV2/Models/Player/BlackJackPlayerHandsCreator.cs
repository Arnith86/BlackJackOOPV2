// Project: BlackJackV2
// file: BlackJackV2/Models/Player/BlackJackPlayerHandsCreator.cs

/// <summary>
/// 
///		static IPlayerHands	CreateBlackJackPlayerHand(HandOwners.HandOwner)				: Returns a player hands object
///		static IPlayer		CreatePlayer(IPlayerHands, ISubject<BetUpdateEvent>, string): Returns a player object
/// 
/// </summary>

using Avalonia.Media.Imaging;
using BlackJackV2.Constants;
using BlackJackV2.Models.CardHand;
using BlackJackV2.Services.Events;
using System.Reactive.Subjects;

namespace BlackJackV2.Models.Player
{
	internal class BlackJackPlayerHandsCreator
	{
		public static IPlayerHands<Bitmap, string> CreateBlackJackPlayerHand( HandOwners.HandOwner id)
		{
			return new PlayerHands(id, BlackJackCardHandCreator.CreateBlackJackCardHand());
		}

		public static IPlayer CreatePlayer(IPlayerHands<Bitmap, string> hands, ISubject<BetUpdateEvent> betUpdateSubject, string name = "Player")
		{
			return new Player(name, hands, betUpdateSubject);
		}
	}
}
