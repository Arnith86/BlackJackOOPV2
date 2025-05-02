using BlackJackV2.ViewModels.Interfaces;

namespace BlackJackV2.ViewModels
{
	/// <summary>
	/// A view model wrapper that combines bet input and action button view models
	/// for simplified binding and player interaction coordination.
	/// </summary>
	public class InputWrapperViewModel : IInputWrapperViewModel
	{
		/// <inheritdoc/>
		public IButtonViewModel ButtonViewModel { get; }
		
		/// <inheritdoc/>
		public IBetViewModel BetViewModel { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="InputWrapperViewModel"/> class.
		/// </summary>
		/// <param name="buttonViewModel">The view model for player action buttons.</param>
		/// <param name="betViewModel">The view model for handling bet input.</param>
		public InputWrapperViewModel(IButtonViewModel buttonViewModel, IBetViewModel betViewModel)
		{
			ButtonViewModel = buttonViewModel;
			BetViewModel = betViewModel;
		}
		
	}
}
