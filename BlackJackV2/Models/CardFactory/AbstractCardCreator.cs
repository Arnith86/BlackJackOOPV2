using Avalonia.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackV2.Models.CardFactory
{
	/* *
	 * Abstract creator class to create cards. Part of the Card factory pattern
	 * */
	internal abstract class AbstractCardCreator<TFrontImage, TBackImage, TValue>
	{
		public abstract ICard<TFrontImage, TBackImage, TValue> CreateCard(TFrontImage frontImage, TBackImage backImage, TValue value);
	}
}
