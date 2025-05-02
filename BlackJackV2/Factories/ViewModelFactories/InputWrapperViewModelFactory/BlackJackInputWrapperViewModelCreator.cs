// Project: BlackJackV2
// File: BlackJackV2/Factories/InputWrapperViewModelFactory/BlackJackInputWrapperViewModelCreator.cs

using BlackJackV2.ViewModels;
using BlackJackV2.ViewModels.Interfaces;

namespace BlackJackV2.Factories.ViewModelFactories.InputWrapperViewModelFactory
{
	/// <summary>
	/// Concrete factory for creating <see cref="InputWrapperViewModel"/> instances
	/// for use in a Blackjack game.
	/// </summary>
	public class BlackJackInputWrapperViewModelCreator : InputWrapperViewModelCreatorBase
	{
		/// <inheritdoc/>
		public override IInputWrapperViewModel CreateInputWrapperViewModel(
			IBetViewModel betViewModel,
			IButtonViewModel buttonViewModel)
		{
			return new InputWrapperViewModel(buttonViewModel, betViewModel);
		}
	}
}