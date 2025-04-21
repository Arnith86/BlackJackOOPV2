// Project BlackJackV2
// file: BlackJackV2/Factories/PlayerHandsFactory/BlackJackPlayerHandsCreator.cs

/// <summary>
///		Part of PlayerHands pattern
///		Concreate creator for BlackJackPlayerHands object creation.
/// </summary>

using Avalonia.Media.Imaging;
using BlackJackV2.Constants;
using BlackJackV2.Factories.CardHandFactory;
using BlackJackV2.Models.CardHand;
using BlackJackV2.Models.PlayerHands;


namespace BlackJackV2.Factories.PlayerHandsFactory
{
	public class BlackJackPlayerHandsCreator : IBlackJackPlayerHandsCreator<Bitmap, string>
	{
		public override IBlackJackPlayerHands<Bitmap, string> CreatePlayerHands(HandOwners.HandOwner id, ICardHandCreator<Bitmap, string> cardHandCreator)
		{
			return new BlackJackPlayerHands(id, cardHandCreator);
		}
	}
}
