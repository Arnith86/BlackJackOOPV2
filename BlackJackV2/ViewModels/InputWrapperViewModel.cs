using BlackJackV2.ViewModels.Interfaces;
using System;

namespace BlackJackV2.ViewModels
{
	public class InputWrapperViewModel
	{
		public IButtonViewModel ButtonViewModel { get; }
		public BetViewModel BetViewModel { get; }

		public InputWrapperViewModel(IButtonViewModel buttonViewModel, BetViewModel betViewModel)
		{
			ButtonViewModel = buttonViewModel;
			BetViewModel = betViewModel;
		}
		
	}
}
