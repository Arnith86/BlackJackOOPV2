// Project BlackJackV2
// file: BlackJackV2/Factories/CardHandViewModelFactory/BlackJackCardHandViewModelCreator.cs

using Avalonia.Media.Imaging;
using BlackJackV2.Models.CardHand;
using BlackJackV2.ViewModels;
using BlackJackV2.ViewModels.Interfaces;

namespace BlackJackV2.Factories.CardHandViewModelFactory
{
	/// <summary>
	/// Concrete factory for creating <see cref="CardHandViewModel"/> instances using Blackjack-specific types.
	/// </summary>
	/// <remarks>
	/// Related files <see cref="BlackJackV2.Factories.CardHandViewModelFactory"/>
	/// </remarks>
	public class BlackJackCardHandViewModelCreator : CardViewModelCreatorBase<Bitmap, string>
	{
		/// <summary>
		/// Creates a <see cref="CardHandViewModel"/> for a given Blackjack card hand model.
		/// </summary>
		/// <param name="blackJackCardHand">The Blackjack card hand to wrap in a ViewModel.</param>
		/// <param name="buttonViewModel">The ViewModel for action buttons associated with the hand.</param>
		/// <returns>An instance of <see cref="ICardHandViewModel"/> representing the card hand.</returns>
		public override ICardHandViewModel CreateCardHandViewModel(IBlackJackCardHand<Bitmap, string> blackJackCardHand, IButtonViewModel buttonViewModel)
		{
			return new CardHandViewModel(blackJackCardHand, buttonViewModel);
		}
	}
}
