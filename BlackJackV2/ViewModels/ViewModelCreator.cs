// Project: BlackJackV2
// file: BlackJackV2/ViewModels/ViewModelCreator.cs

using BlackJackV2.Factories.ViewModelFactories.BetViewModelFactory;
using BlackJackV2.Factories.ViewModelFactories.ButtonViewModelFactory;
using BlackJackV2.Factories.ViewModelFactories.CardHandViewModelFactory;
using BlackJackV2.Factories.ViewModelFactories.PlayerViewModelFactory;
using BlackJackV2.ViewModels.Interfaces;

namespace BlackJackV2.ViewModels
{
	/// <summary>
	/// A wrapper for concrete ViewModel creators for use in a Blackjack game.
	/// </summary>
	/// <remarks>
	/// Related files <see cref="ViewModels.Interfaces"/>
	/// </remarks>
	public record ViewModelCreator(
		BlackJackBetViewModelCreator BlackJackBetViewModelCreator,
		BlackJackButtonViewModelCreator BlackJackButtonViewModelCreator,
		BlackJackCardHandViewModelCreator BlackJackCardHandViewModelCreator,
		BlackJackPlayerViewModelCreator BlackJackPlayerViewModelCreator
	) : IViewModelCreator;
}
