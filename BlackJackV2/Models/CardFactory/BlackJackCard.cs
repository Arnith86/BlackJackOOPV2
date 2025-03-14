using Avalonia.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackV2.Models.CardFactory
{

	/**
	 * A single card used in black jack. 
	 * 
	 *  Bitmap CurrentCardImage	the current image that shown (front or back of card)
	 *	Bitmap FrontImage:		the visual representation of the card value
	 *	Bitmap BackImage:		the visual representation of the back of the card value 
	 *	string[] Value:			has one of four colors ( Heart, Dimond, Spade, Club ), and a numerical value between 1 - 13 (knight: 11, queen: 12, king: 13)
	 *	bool FaceDown:			signifies if the back of the card is shown or the value
	 * 
	 **/
	public class BlackJackCard : ICard<Bitmap, Bitmap, string[]>
	{
		public Bitmap CurrentCardImage => FaceDown ? BackImage : FrontImage;
		public Bitmap FrontImage { get; private set; }
		public Bitmap BackImage { get; private set; }
		public string[] Value { get; private set; }
		public bool FaceDown { get; private set; }

		public BlackJackCard(Bitmap frontImage, Bitmap backImage, string[] value)
		{
			FrontImage = frontImage;
			BackImage = backImage;
			Value = value;
			FaceDown = true;
		}

		// Initiates card flips
		public void FlipCard()
		{
			FaceDown = !FaceDown;
		}
	}
}
