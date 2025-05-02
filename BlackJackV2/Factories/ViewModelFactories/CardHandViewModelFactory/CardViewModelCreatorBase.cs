// Project BlackJackV2
// file: BlackJackV2/Factories/CardHandViewModelFactory/CardViewModelCreatorBase.cs

using BlackJackV2.Models.CardHand;
using BlackJackV2.ViewModels;
using BlackJackV2.ViewModels.Interfaces;

namespace BlackJackV2.Factories.ViewModelFactories.CardHandViewModelFactory
{
	/// <summary>
	/// Abstract base class for factories that create <see cref="ICardHandViewModel"/> instances.
	/// Provides a generic contract for converting a Blackjack card hand model into its corresponding ViewModel.
	/// </summary>
	/// <remarks>
	/// Related files <see cref="ViewModels.Interfaces"/>
	/// </remarks>
	/// <typeparam name="TImage">The type used to represent card images (e.g., Bitmap).</typeparam>
	/// <typeparam name="TValue">The type used to represent card values (e.g., string).</typeparam>
	public abstract class CardViewModelCreatorBase<TImage, TValue>
	{
		/// <summary>
		/// Creates a ViewModel for the given Blackjack card hand model.
		/// </summary>
		/// <param name="blackJackCardHand">The card hand model to base the ViewModel on.</param>
		/// <param name="inputWrapperViewModel">Holds the ViewModels for buttons and bet registration.</param>
		/// <returns>An instance of <see cref="ICardHandViewModel"/> representing the given hand.</returns>
		public abstract ICardHandViewModel CreateCardHandViewModel(
			IBlackJackCardHand<TImage, TValue> blackJackCardHand,
			InputWrapperViewModel inputWrapperViewModel);

		/// <summary>
		/// Creates a ViewModel for the dealers Blackjack card hand model.
		/// </summary>
		/// <param name="blackJackCardHand">The card hand model assigned to the dealer to base the ViewModel on.</param>
		/// <returns>An instance of <see cref="ICardHandViewModel"/> representing the given hand.</returns>
		public abstract ICardHandViewModel CreateDealerCardHandViewModel(IBlackJackCardHand<TImage, TValue> blackJackCardHand);
	}
}

