// Project: BlackJackV2
// file: BlackJackV2/Models/CardFactory/BlackJackCard.cs

using ReactiveUI;
using System;
using System.Reactive.Disposables;

namespace BlackJackV2.Models.Card
{
	/// <summary>
	/// Represents a single card used in Blackjack.
	/// Combines front and back images, a suit/value string, and face-down state.
	/// Implements the <see cref="ICard{TImage, TValue}"/> interface for use in a card factory.
	/// </summary>
	/// <remarks> Related files <see cref="BlackJackV2.Factories.CardFactory"/></remarks>
	public class BlackJackCard<TImage, TValue> : ReactiveObject,  ICard<TImage, TValue>
	{
		/// <inheritdoc/>
		public TImage CurrentImage => FaceDown ? BackImage : FrontImage;

		/// <inheritdoc/>
		public TImage FrontImage { get; private set; }

		/// <inheritdoc/>
		public TImage BackImage { get; private set; }

		/// <summary>
		/// Gets the the string representation of the card's value (e.g., "Hearts_10", "Diamons_Knight"). 
		/// </summary>
		public TValue Value { get; private set; }
		
		/// <summary>
		/// Gets or sets a value indicating whether the card is face down.
		/// Changing this will automatically update the <see cref="CurrentImage"/>.
		/// </summary>
		public bool FaceDown 
		{
			get => _faceDown;
			set
			{
				this.RaiseAndSetIfChanged(ref _faceDown, value);
				// Raise change for CurrentImage when FaceDown changes
				this.RaisePropertyChanged(nameof(CurrentImage)); 
			}
		}
		private bool _faceDown;

		/// <summary>
		/// Used to manage and dispose of reactive subscriptions tied to the cards.
		/// </summary>
		private readonly CompositeDisposable _disposables = new CompositeDisposable();


		/// <summary>
		/// Initializes a new instance of the <see cref="BlackJackCard"/> class.
		/// </summary>
		/// <param name="frontImage">The image representing the front of the card.</param>
		/// <param name="backImage">The image representing the back of the card.</param>
		/// <param name="value">The string value of the card (e.g., "Spades_Knight" or "Hearts_2").</param>
		public BlackJackCard(TImage frontImage, TImage backImage, TValue value)
		{
			FrontImage = frontImage;
			BackImage = backImage;
			Value = value;
			_faceDown = false;

			// Raise change for CurrentImage when FaceDown changes
			this.WhenAnyValue(x => x.FaceDown)
				.Subscribe( _=>	this.RaisePropertyChanged(nameof(FaceDown)))
				.DisposeWith(_disposables);
		}

		/// <summary>
		/// Flips the card by toggeling the <see cref="FaceDown"/> property.
		/// </summary>
		public void FlipCard()
		{
			FaceDown = !FaceDown;
		}

		/// <summary>
		/// Disposes of any reactive subscriptions associated with this card.
		/// </summary>
		public void Dispose()
		{
			_disposables.Dispose();
		}
	}
}
