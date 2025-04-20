// Project: BlackJackV2
// file: BlackJackV2/Models/CardFactory/ICard.cs


/// <summary>
///
///		This is the "Product" part of the "Card" factory. 
///		Common aspect to all cards is image, generic types are used to ease reuse of this interface
///		
///		CurrentImage	: The current side image of the card
///		FrontImage		: The front side image of the card
///		BackImage		: The back side image of the card
///		Value			: The value of the card
///		FaceDown		: The state of the card, true if the card is face down	
/// 
///		FlipCard()		: Flips the card to the other side
///		
/// </summary>
namespace BlackJackV2.Models.Card
{
	public interface ICard<TImage, TValue>
	{
		public TImage CurrentImage{ get; }
		public TImage FrontImage { get; }
		public TImage BackImage { get; }
		public TValue Value { get; }
		public bool FaceDown { get; }
		public void FlipCard();
	}
}
