// Project BlackJackV2
// file: BlackJackV2/Factories/PlayerViewModelFactory/PlayerViewModelCreatorBase.cs

using BlackJackV2.ViewModels.Interfaces;
using BlackJackV2.Models.Player;
using System.Reactive.Subjects;
using BlackJackV2.Services.Events;
using BlackJackV2.Models.GameLogic.PlayerServices;
using BlackJackV2.Models.GameLogic.GameRuleServices;


namespace BlackJackV2.Factories.ViewModelFactories.PlayerViewModelFactory
{
	/// <summary>
	/// Defines the abstract base for a factory that creates player view models
	/// for use in the Blackjack UI. Concrete implementations determine how the
	/// view model is constructed for a specific player type or configuration.
	/// </summary>
	/// <remarks>
	/// Related files <see cref="ViewModels.Interfaces"/>
	/// </remarks>
	/// <typeparam name="TImage">The image type used to represent cards.</typeparam>
	/// <typeparam name="TValue">The value type used to represent card values.</typeparam>
	public abstract class PlayerViewModelCreatorBase<TImage, TValue>
	{
		/// <summary>
		/// Creates an instance of <see cref="IPlayerViewModel"/> for a given player.
		/// Concrete implementations should provide the full construction logic, including
		/// reactive event wiring for splits and bet updates.
		/// </summary>
		/// <param name="player">The player model used to build the view model.</param>
		/// <param name="splitSuccessfulEvent">Event triggered when a player successfully splits their hand.</param>
		/// <param name="betUpdateEvent">Event triggered when a player's bet is updated.</param>
		/// <param name="betRequestEvent">Event triggered when a bet is requested from the player.</param>
		/// <param name="viewModelCreator">A wrapper class containing factories for creating view models.</param>
		/// <param name="playerServices">Handles player-specific services and actions.</param>
		/// <param name="gameRuleServices">Handles game rules.</param>
		/// <returns>A fully constructed and reactive <see cref="IPlayerViewModel"/> instance.</returns>
		public abstract IPlayerViewModel CreatePlayerViewModel(	
			IPlayer<TImage, TValue> player, 
			Subject<SplitEvent> splitSuccessfulEvent, 
			Subject<BetUpdateEvent> betUpdateEvent,
			Subject<BetRequestEvent<TImage, TValue>> betRequestEvent,
			IViewModelCreator viewModelCreator,
			IPlayerServices<TImage, TValue> playerServices,
			GameRuleServices<TImage, TValue> gameRuleServices);
	}
}
