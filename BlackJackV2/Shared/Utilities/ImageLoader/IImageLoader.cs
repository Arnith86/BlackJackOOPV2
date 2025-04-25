// Project: BlackJackV2
// file: BlackJackV2/Shared/Utilities/ImageLoader/IImageLoader.cs

namespace BlackJackV2.Shared.Utilities.ImageLoader
{
	/// <summary>
	/// Interface for loading images from a URI.
	/// </summary>
	/// <typeparam name="TImage">The type of image that will be returned by the loader (e.g., Bitmap).</typeparam>
	public interface IImageLoader<TImage>
	{
		/// <summary>
		/// Loads an image from the provided URI.
		/// </summary>
		/// <param name="uri">The URI pointing to the image resource.</param>
		/// <returns>The loaded image of type TImage.</returns>
		TImage Load(string uri);
	}
}
