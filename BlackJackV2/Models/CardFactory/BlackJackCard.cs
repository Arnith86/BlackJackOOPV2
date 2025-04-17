// Project: BlackJackV2
// file: BlackJackV2/Models/CardFactory/BlackJackCard.cs

/// <summary>
///		
///		A single card used in black jack. 
/// 
///		Bitmap		CurrentCardImage	:	The current image that shown (front or back of card).
///		Bitmap		FrontImage			:	The visual representation of the card value.
///		Bitmap		BackImage			:	The visual representation of the back of the card value 
///		string[]	Value				:   Has one of four colors ( Heart, Dimond, Spade, Club ), 
///											and a numerical value between 1 - 13 (knight: 11, queen: 12, king: 13)
///		bool		FaceDown			:	Signifies if the back of the card is shown or the value
///		
///		void		flipCard()			:	Flips the card to show the opposite side	
///		
/// </summary>

using Avalonia.Media.Imaging;
using ReactiveUI;
using System;

namespace BlackJackV2.Models.CardFactory
{
	public class BlackJackCard : ReactiveObject,  ICard<Bitmap, string>
	{
		public Bitmap CurrentImage => FaceDown ? BackImage : FrontImage;
		public Bitmap FrontImage { get; private set; }
		public Bitmap BackImage { get; private set; }
		public string Value { get; private set; }
		
		private bool _faceDown;

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

		public BlackJackCard(Bitmap frontImage, Bitmap backImage, string value)
		{
			FrontImage = frontImage;
			BackImage = backImage;
			Value = value;
			_faceDown = false;

			// Raise change for CurrentImage when FaceDown changes
			this.WhenAnyValue(x => x.FaceDown).Subscribe( _=> this.RaisePropertyChanged(nameof(FaceDown)));
		}

		// Initiates card flips
		public void FlipCard()
		{
			FaceDown = !FaceDown;
		}
	}
}
