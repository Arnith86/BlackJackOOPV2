﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackV2.Models
{
	/* *
	 * Abstract creator class to create cards. Part of the Card factory pattern
	 * */
	internal abstract class CardCreator<TFrontImage, TBackImage, TValue>
	{
		public abstract ICard<TFrontImage, TBackImage, TValue> CardFactory();
	}
}
