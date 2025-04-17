// Project: BlackJackV2
// file: BlackJackV2/Models/CardFactory/ICardCreator.cs

/// <summary>
///		Interface for the creator class to create cards. Part of the Card factory pattern 
/// </summary>

namespace BlackJackV2.Models.CardFactory
{
	internal abstract class ICardCreator<TImage, TValue>
	{
		public abstract ICard<TImage, TValue> CreateCard(TImage frontImage, TImage backImage, TValue value);
	}
}
