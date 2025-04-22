// Project: BlackJackV2
// file: BlackJackV2/Models/CardFactory/ICard.cs


namespace BlackJackV2.Models.Card
{
	/// <summary>
	///	Represents a playing card with a front and back image, a value, and a face-down state.
	///	This interface serves as the product in the Card Factory pattern, using generics for image and value types to support reuse across games.
	///	</summary>
	/// <typeparam name="TImage">The type representing the image used on the card (e.g., bitmap).</typeparam>
	/// <typeparam name="TValue">The type representing the card's value (e.g., string, int).</typeparam>
	public interface ICard<TImage, TValue>
	{
		/// <summary>
		/// Gets the image currently displayed (front or back), dependings on the value of <see cref="FaceDown"/>.
		/// </summary>
		public TImage CurrentImage { get; }

		/// <summary>
		/// Gets the image representing the front side of the card (i.e., card face).
		/// </summary>
		public TImage FrontImage { get; }

		/// <summary>
		/// Gets the image representing the back side of the card.
		/// </summary>
		public TImage BackImage { get; }

		/// <summary>
		/// Gets the logical value of the card (e.g., "Ace", "10", "King").
		/// </summary>
		public TValue Value { get; }

		/// <summary>
		/// Gets a value indicating whether the card is currently face down.
		/// </summary>
		public bool FaceDown { get; }

		/// <summary>
		/// Flips the card, toggling between face up and face down.
		/// </summary>
		void FlipCard();
	}
}
