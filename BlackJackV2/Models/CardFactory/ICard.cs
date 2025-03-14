

using Avalonia.Media.Imaging;

namespace BlackJackV2.Models.CardFactory
{

	/* *
	 * This is the "Product" part of the "Card" factory. 
	 * Common aspect to all cards is image, generic types are used to ease reuse of this interface
	 * */
	internal interface ICard<TFrontImage, TBackImage, TValue>
	{
		public TFrontImage FrontImage { get; }
		public TBackImage BackImage { get; }
		public TValue Value { get; }
	}
}
