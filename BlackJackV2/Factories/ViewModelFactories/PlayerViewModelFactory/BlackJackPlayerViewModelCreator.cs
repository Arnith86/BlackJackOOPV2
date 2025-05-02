// Project BlackJackV2
// file: BlackJackV2/Factories/PlayerViewModelFactory/BlackJackPlayerViewModelCreator.cs

using Avalonia.Media.Imaging;
using BlackJackV2.Factories.ViewModelFactories.ButtonViewModelFactory;
using BlackJackV2.Factories.ViewModelFactories.CardHandViewModelFactory;
using BlackJackV2.Models.GameLogic.GameRuleServices;
using BlackJackV2.Models.GameLogic.PlayerServices;
using BlackJackV2.Models.Player;
using BlackJackV2.Services.Events;
using BlackJackV2.ViewModels;
using BlackJackV2.ViewModels.Interfaces;
using System.Reactive.Subjects;

namespace BlackJackV2.Factories.ViewModelFactories.PlayerViewModelFactory
{
	/// <summary>
	/// Concrete factory for creating Blackjack-specific player view models.
	/// Uses <see cref="Bitmap"/> for card images and <see cref="string"/> for card values.
	/// </summary>
	/// <remarks>
	/// Related files <see cref="ViewModels.Interfaces"/>
	/// </remarks>
	public class BlackJackPlayerViewModelCreator : PlayerViewModelCreatorBase<Bitmap, string>
	{
		/// <summary>
		/// Creates a <see cref="PlayerViewModel"/> configured for Blackjack,
		/// wiring up reactive event streams for split and bet updates.
		/// </summary>
		/// <param name="player">The player model used to build the view model.</param>
		/// <param name="splitSuccessfulEvent">Event triggered when a player successfully splits their hand.</param>
		/// <param name="betUpdateEvent">Event triggered when a player's bet is updated.</param>
		/// <param name="betRequestEvent">Event triggered when a bet is requested from the player.</param>
		/// <param name="viewModelCreator">A wrapper class containing factories for creating view models.</param>
		/// <param name="playerServices">Handles player-specific services and actions.</param>
		/// <param name="gameRuleServices">Handles game rules.</param>
		/// <returns>A fully configured <see cref="PlayerViewModel"/> instance.</returns>
		public override IPlayerViewModel CreatePlayerViewModel(
			IPlayer<Bitmap, string> player,
			Subject<SplitSuccessfulEvent> splitSuccessfulEvent,
			Subject<BetUpdateEvent> betUpdateEvent,
			Subject<BetRequestEvent<Bitmap, string>> betRequestEvent,
			IViewModelCreator viewModelCreator,
			IPlayerServices<Bitmap, string> playerServices,
			GameRuleServices<Bitmap, string> gameRuleServices)
		{
			return new PlayerViewModel(	
				player, 
				splitSuccessfulEvent, 
				betUpdateEvent,
				betRequestEvent,
				viewModelCreator,
				playerServices, 
				gameRuleServices
			);
		}
	}
}
