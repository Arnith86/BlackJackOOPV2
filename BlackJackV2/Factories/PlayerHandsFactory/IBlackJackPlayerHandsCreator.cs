// Project BlackJackV2
// file: BlackJackV2/Factories/PlayerHandsFactory/IBlackJackPlayerHandsCreator.cs

/// <summary>
///		Part of the PlayerHands factory pattern 
///		Template for BlackJackPlayerHands object creation.
/// </summary>

using Avalonia.Media.Imaging;
using BlackJackV2.Constants;
using BlackJackV2.Factories.CardHandFactory;
using BlackJackV2.Models.CardHand;
using BlackJackV2.Models.PlayerHands;

namespace BlackJackV2.Factories.PlayerHandsFactory
{
	public abstract class IBlackJackPlayerHandsCreator<TImage, TValue>
	{
		public abstract IBlackJackPlayerHands<TImage, TValue> CreatePlayerHands(HandOwners.HandOwner id, ICardHandCreator<Bitmap, string> cardHandCreator);
	}
}
