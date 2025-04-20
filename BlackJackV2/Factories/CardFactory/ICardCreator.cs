// Project: BlackJackV2
// file: BlackJackV2/Factories/CardFactory/ICardCreator.cs

/// <summary>
///		Interface for the creator class to create cards. Part of the Card factory pattern 
/// </summary>
/// 
using BlackJackV2.Models.Card;

namespace BlackJackV2.Factories.CardFactory
{
	public abstract class ICardCreator<TImage, TValue>
	{
		public abstract ICard<TImage, TValue> CreateCard(TImage frontImage, TImage backImage, TValue value);
	}
}
