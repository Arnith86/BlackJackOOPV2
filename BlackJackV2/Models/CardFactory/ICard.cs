

using Avalonia.Media.Imaging;

namespace BlackJackV2.Models
{

	/**
	 * This is the "Product" part of the "Card" factory. 
	 * Common aspect to all cards is image, generic types are used to ease reuse of this interface
	 **/

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
