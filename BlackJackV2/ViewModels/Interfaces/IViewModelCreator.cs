// Project: BlackJackV2
// file: BlackJackV2/ViewModels/Interfaces/IViewModelCreator.cs

using BlackJackV2.Factories.ViewModelFactories.BetViewModelFactory;
using BlackJackV2.Factories.ViewModelFactories.ButtonViewModelFactory;
using BlackJackV2.Factories.ViewModelFactories.CardHandViewModelFactory;
using BlackJackV2.Factories.ViewModelFactories.InputWrapperViewModelFactory;
using BlackJackV2.Factories.ViewModelFactories.PlayerViewModelFactory;

namespace BlackJackV2.ViewModels.Interfaces
{
	/// <summary>
	/// Interface for accessing ViewModel factory components used in the Blackjack game.
	/// </summary>
	public interface IViewModelCreator
	{
		BlackJackBetViewModelCreator BlackJackBetViewModelCreator { get; }
		BlackJackButtonViewModelCreator BlackJackButtonViewModelCreator { get; }
		BlackJackCardHandViewModelCreator BlackJackCardHandViewModelCreator { get; }
		BlackJackPlayerViewModelCreator BlackJackPlayerViewModelCreator { get; }
		BlackJackInputWrapperViewModelCreator BlackJackInputWrapperViewModelCreator { get; }
	}
}