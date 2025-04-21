// Project BlackJackV2
// file: BlackJackV2/Factories/PlayerFactory/IPlayerCreator.cs

/// <summary>
///		Part of the Player factory pattern 
///		Template for Player object creation.
/// </summary>

using BlackJackV2.Models.Player;
using BlackJackV2.Models.PlayerHands;
using BlackJackV2.Services.Events;
using System.Reactive.Subjects;

namespace BlackJackV2.Factories.PlayerFactory
{
	public abstract class IPlayerCreator<TImage, TValue>
	{
		public abstract IPlayer CreatePlayer(IBlackJackPlayerHands<TImage, TValue> playerHands, ISubject<BetUpdateEvent> betUpdatedSubject, string name);
	}
}
