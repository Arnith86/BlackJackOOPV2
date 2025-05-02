using BlackJackV2.ViewModels.Interfaces;

namespace BlackJackV2.ViewModels
{
	public class InputWrapperViewModel
	{
		public IButtonViewModel ButtonViewModel { get; }
		public IBetViewModel BetViewModel { get; }

		public InputWrapperViewModel(IButtonViewModel buttonViewModel, IBetViewModel betViewModel)
		{
			ButtonViewModel = buttonViewModel;
			BetViewModel = betViewModel;
		}
		
	}
}
