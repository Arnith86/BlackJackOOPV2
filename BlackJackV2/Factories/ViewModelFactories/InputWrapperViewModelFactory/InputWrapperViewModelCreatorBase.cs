// Project BlackJackV2
// file: BlackJackV2/Factories/InputWrapperViewModelFactory/InputWrapperViewModelCreatorBase.cs

using BlackJackV2.ViewModels.Interfaces;

namespace BlackJackV2.Factories.ViewModelFactories.InputWrapperViewModelFactory
{
	/// <summary>
	/// Abstract factory base class for creating instances of <see cref="IInputWrapperViewModel"/>.
	/// </summary>
	public abstract class InputWrapperViewModelCreatorBase
	{
		/// <summary>
		/// Creates an <see cref="IInputWrapperViewModel"/> using the provided bet and button view models.
		/// </summary>
		/// <param name="betViewModel">The view model for handling player bets.</param>
		/// <param name="buttonViewModel">The view model for handling player actions (e.g., Hit, Stand).</param>
		/// <returns>An instance of <see cref="IInputWrapperViewModel"/>.</returns>
		public abstract IInputWrapperViewModel CreateInputWrapperViewModel(
			IBetViewModel betViewModel, 
			IButtonViewModel buttonViewModel);
	}
}
