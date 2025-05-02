// Project: BlackJackV2
// file: BlackJackV2/ViewModels/Interfaces/IViewModelCreator.cs

using Avalonia.Media.Imaging;
using BlackJackV2.Factories.ViewModelFactories.BetViewModelFactory;
using BlackJackV2.Factories.ViewModelFactories.ButtonViewModelFactory;
using BlackJackV2.Factories.ViewModelFactories.CardHandViewModelFactory;
using BlackJackV2.Factories.ViewModelFactories.InputWrapperViewModelFactory;
using BlackJackV2.Factories.ViewModelFactories.PlayerViewModelFactory;
using BlackJackV2.Models.GameLogic.GameRuleServices;
using BlackJackV2.Models.GameLogic.PlayerServices;
using BlackJackV2.Models.Player;
using BlackJackV2.Shared.Constants;

namespace BlackJackV2.ViewModels.Interfaces
{
	/// <summary>
	/// Interface for accessing ViewModel factory components used in the Blackjack game.
	/// </summary>
	public interface IViewModelCreator
	{
		/// <summary>
		/// Factory responsible for creating <see cref="IBetViewModel"/> instances.
		/// </summary>
		BlackJackBetViewModelCreator BlackJackBetViewModelCreator { get; }

		/// <summary>
		/// Factory responsible for creating <see cref="IButtonViewModel"/> instances.
		/// </summary>
		BlackJackButtonViewModelCreator BlackJackButtonViewModelCreator { get; }
		
		/// <summary>
		/// Factory responsible for creating <see cref="ICardHandViewModel"/> instances.
		/// </summary>
		BlackJackCardHandViewModelCreator BlackJackCardHandViewModelCreator { get; }


		/// <summary>
		/// Factory responsible for creating <see cref="IPlayerViewModel"/> instances.
		/// </summary>
		BlackJackPlayerViewModelCreator BlackJackPlayerViewModelCreator { get; }

		/// <summary>
		/// Factory responsible for creating <see cref="IInputWrapperViewModel"/> instances.
		/// </summary>
		BlackJackInputWrapperViewModelCreator BlackJackInputWrapperViewModelCreator { get; }

		/// <summary>
		/// Builds a <see cref="ICardHandViewModel"/> for a given player's hand (primary or split).
		/// </summary>
		/// <param name="primaryOrSplit">Specifies whether to build for the primary or split hand.</param>
		/// <param name="player">The player owning the hand.</param>
		/// <param name="playerServices">Services associated with player operations.</param>
		/// <param name="gameRuleServices">Services providing game rules logic.</param>
		/// <returns>The fully constructed <see cref="ICardHandViewModel"/>.</returns>
		public ICardHandViewModel BuildCardHandViewModel(
			HandOwners.HandOwner primaryOrSplit,
			IPlayer<Bitmap, string> player,
			IPlayerServices<Bitmap, string> playerServices,
			GameRuleServices<Bitmap, string> gameRuleServices);
	}
}