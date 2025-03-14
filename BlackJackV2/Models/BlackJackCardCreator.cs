using Avalonia.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackV2.Models
{
	internal class BlackJackCardCreator : AbstractCardCreator<Bitmap, Bitmap, string[]>
	{
		public override ICard<Bitmap, Bitmap, string[]> CreateCard(Bitmap frontImage, Bitmap backImage, string[] value)
		{
			return new BlackJackCard(frontImage, backImage, value);
		}
	}
}
