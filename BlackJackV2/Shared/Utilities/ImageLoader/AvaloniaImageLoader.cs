// Project: BlackJackV2
// file: BlackJackV2/Shared/Utilities/ImageLoader/AvaloniaImageLoader.cs

using Avalonia.Media.Imaging;
using Avalonia.Platform;
using System;

namespace BlackJackV2.Shared.Utilities.ImageLoader
{
	/// <summary>
	/// An implementation of IImageLoader for Avalonia that loads images as Bitmap.
	/// </summary>
	public class AvaloniaImageLoader : IImageLoader<Bitmap>
	{
		/// <summary>
		/// Loads an image from the provided URI and returns a Bitmap object.
		/// </summary>
		/// <param name="uri">The URI pointing to the image resource.</param>
		/// <returns>A Bitmap object representing the loaded image.</returns>
		public Bitmap Load(string uri)
		{
			// Convert string URI to a Uri object.
			var assetUri = new Uri(uri);
			// Load the image from the URI using Avalonia's AssetLoader.
			return new Bitmap(AssetLoader.Open(assetUri));
		}
	}
}
