// Project: BlackJackV2
// file: BlackJackV2/Factories/CardHandFactory/ICardHandCreator.cs

/// <summary>
///		Part of CardHandFactory pattern
///		Template for CardHand creation.
/// </summary>

using BlackJackV2.Models.CardHand;

namespace BlackJackV2.Factories.CardHandFactory
{
	public abstract class ICardHandCreator<TImage, TValue>
	{
		public abstract IBlackJackCardHand<TImage, TValue> CreateCardHand();
	}
}
