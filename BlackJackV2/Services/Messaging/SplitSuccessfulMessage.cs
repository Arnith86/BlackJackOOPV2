using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackV2.Services.Messaging
{

	/**
	 * This class is used to send messages between different parts of the application.
	 * It is used to notify if a split action is successful.
	 **/

	public class SplitSuccessfulMessage
	{
		public bool IsSplitSuccessful { get; }

		public SplitSuccessfulMessage(bool isSplitSuccessful)
		{
			IsSplitSuccessful = isSplitSuccessful;
		}
	}
}
