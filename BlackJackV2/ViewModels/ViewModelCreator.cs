// Project: BlackJackV2
// file: BlackJackV2/ViewModels/ViewModelCreator.cs

using Avalonia.Media.Imaging;
using BlackJackV2.Factories.ViewModelFactories.BetViewModelFactory;
using BlackJackV2.Factories.ViewModelFactories.ButtonViewModelFactory;
using BlackJackV2.Factories.ViewModelFactories.CardHandViewModelFactory;
using BlackJackV2.Factories.ViewModelFactories.InputWrapperViewModelFactory;
using BlackJackV2.Factories.ViewModelFactories.PlayerViewModelFactory;
using BlackJackV2.Models.CardHand;
using BlackJackV2.Models.GameLogic.GameRuleServices;
using BlackJackV2.Models.GameLogic.PlayerServices;
using BlackJackV2.Models.Player;
using BlackJackV2.Shared.Constants;
using BlackJackV2.ViewModels.Interfaces;

namespace BlackJackV2.ViewModels
{
	/// <summary>
	/// Centralized wrapper for all ViewModel creation logic in the Blackjack game.
	/// </summary>
	/// <remarks>
	/// This class simplifies the instantiation of ViewModels by providing access
	/// to concrete ViewModel factories. It adheres to the composition root pattern
	/// and promotes maintainability and testability.
	/// </remarks>
	public class ViewModelCreator : IViewModelCreator
	{
		/// <inheritdoc/>
		public BlackJackBetViewModelCreator BlackJackBetViewModelCreator { get; }
		/// <inheritdoc/>
		public BlackJackButtonViewModelCreator BlackJackButtonViewModelCreator { get; }
		/// <inheritdoc/>
		public BlackJackCardHandViewModelCreator BlackJackCardHandViewModelCreator { get; }
		/// <inheritdoc/>
		public BlackJackPlayerViewModelCreator BlackJackPlayerViewModelCreator { get; }
		/// <inheritdoc/>
		public BlackJackInputWrapperViewModelCreator BlackJackInputWrapperViewModelCreator { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="ViewModelCreator"/> class with all required factories.
		/// </summary>
		/// <param name="blackJackBetViewModelCreator">The bet view model factory.</param>
		/// <param name="blackJackButtonViewModelCreator">The button view model factory.</param>
		/// <param name="blackJackCardHandViewModelCreator">The card hand view model factory.</param>
		/// <param name="blackJackPlayerViewModelCreator">The player view model factory.</param>
		/// <param name="blackJackInputWrapperViewModelCreator">The input wrapper view model factory.</param>
		public ViewModelCreator(
			BlackJackBetViewModelCreator blackJackBetViewModelCreator,
			BlackJackButtonViewModelCreator blackJackButtonViewModelCreator,
			BlackJackCardHandViewModelCreator blackJackCardHandViewModelCreator,
			BlackJackPlayerViewModelCreator blackJackPlayerViewModelCreator,
			BlackJackInputWrapperViewModelCreator blackJackInputWrapperViewModelCreator)
		{
			BlackJackBetViewModelCreator = blackJackBetViewModelCreator;
			BlackJackButtonViewModelCreator = blackJackButtonViewModelCreator;
			BlackJackCardHandViewModelCreator = blackJackCardHandViewModelCreator;
			BlackJackPlayerViewModelCreator = blackJackPlayerViewModelCreator;
			BlackJackInputWrapperViewModelCreator = blackJackInputWrapperViewModelCreator;
		}

		/// <inheritdoc/>
		public ICardHandViewModel BuildCardHandViewModel(
			HandOwners.HandOwner primaryOrSplit,
			IPlayer<Bitmap, string> player,
			IPlayerServices<Bitmap, string> playerServices,
			GameRuleServices<Bitmap, string> gameRuleServices)
		{
			IBetViewModel betViewModel =
				BlackJackBetViewModelCreator.CreateBetViewModel(
					player,
					playerServices,
					gameRuleServices.GameRules
				);

			IButtonViewModel buttonViewModel =
				BlackJackButtonViewModelCreator.CreateButtonViewModel(
					player,
					primaryOrSplit,
					playerServices.PlayerRound
				);

			IInputWrapperViewModel inputWrapperViewModel =
				BlackJackInputWrapperViewModelCreator.CreateInputWrapperViewModel(
					betViewModel,
					buttonViewModel
				);

			IBlackJackCardHand<Bitmap, string> hand =
				primaryOrSplit == HandOwners.HandOwner.Primary ?
				player.Hands.PrimaryCardHand : player.Hands.SplitCardHand;

			return	BlackJackCardHandViewModelCreator.
				CreateCardHandViewModel(hand, inputWrapperViewModel);
		}
	}
}