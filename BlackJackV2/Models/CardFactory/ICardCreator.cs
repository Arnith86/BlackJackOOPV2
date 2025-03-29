using Avalonia.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackV2.Models.CardFactory
{
	/**
	 * Interface for the creator class to create cards. Part of the Card factory pattern
	 **/
	internal abstract class ICardCreator<TImage, TValue>
	{
		public abstract ICard<TImage, TValue> CreateCard(TImage frontImage, TImage backImage, TValue value);
	}
}
