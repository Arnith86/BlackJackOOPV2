﻿// Project BlackJackV2
// file: BlackJackV2/Factories/CardHandViewModelFactory/BlackJackCardHandViewModelCreator.cs

using Avalonia.Media.Imaging;
using BlackJackV2.Models.CardHand;
using BlackJackV2.ViewModels;
using BlackJackV2.ViewModels.Interfaces;

namespace BlackJackV2.Factories.ViewModelFactories.CardHandViewModelFactory
{
	/// <summary>
	/// Concrete factory for creating <see cref="CardHandViewModel"/> instances using Blackjack-specific types.
	/// </summary>
	/// <remarks>
	/// Related files <see cref="ViewModels.Interfaces"/>
	/// </remarks>
	public class BlackJackCardHandViewModelCreator : CardViewModelCreatorBase<Bitmap, string>
	{
		/// <summary>
		/// Creates a <see cref="CardHandViewModel"/> for a given Blackjack card hand model.
		/// </summary>
		/// <param name="blackJackCardHand">The Blackjack card hand to wrap in a ViewModel.</param>
		/// <param name="inputWrapperViewModel">Holds the ViewModels for buttons and bet registration.</param>
		/// <returns>An instance of <see cref="ICardHandViewModel"/> representing the card hand.</returns>
		public override ICardHandViewModel CreateCardHandViewModel(
			IBlackJackCardHand<Bitmap, string> blackJackCardHand,
			IInputWrapperViewModel inputWrapperViewModel)
		{
			return new CardHandViewModel(blackJackCardHand, inputWrapperViewModel);
		}
		
		/// <inheritdoc />
		public override ICardHandViewModel CreateDealerCardHandViewModel(IBlackJackCardHand<Bitmap, string> blackJackCardHand)
		{
			return new CardHandViewModel(blackJackCardHand);
		}
	}
}
