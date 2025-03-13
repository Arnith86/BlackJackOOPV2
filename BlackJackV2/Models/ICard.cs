using Avalonia.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackV2.Models
{

	/* *
	 * This is the "Product" part of the "Card" factory. 
	 * Common aspect to all cards is image, generic types are used to ease reuse of this interface
	 * */
	internal interface ICard<TImage, TValue>
	{
		public TImage Image{ get; set; }
		public TValue Value { get; set; }
	}
}
