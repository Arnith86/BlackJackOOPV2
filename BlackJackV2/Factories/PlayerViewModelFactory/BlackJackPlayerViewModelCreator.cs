// Project BlackJackV2
// file: BlackJackV2/Factories/PlayerViewModelFactory/BlackJackPlayerViewModelCreator.cs

using Avalonia.Media.Imaging;
using BlackJackV2.Factories.ButtonViewModelFactory;
using BlackJackV2.Factories.CardHandViewModelFactory;
using BlackJackV2.Models.GameLogic.PlayerServices;
using BlackJackV2.Models.Player;
using BlackJackV2.Services.Events;
using BlackJackV2.ViewModels;
using BlackJackV2.ViewModels.Interfaces;
using System.Reactive.Subjects;

namespace BlackJackV2.Factories.PlayerViewModelFactory
{
	/// <summary>
	/// Concrete factory for creating Blackjack-specific player view models.
	/// Uses <see cref="Bitmap"/> for card images and <see cref="string"/> for card values.
	/// </summary>
	/// <remarks>
	/// Related files <see cref="BlackJackV2.ViewModels.Interfaces"/>
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
		/// <param name="blackJackCardHandViewModelCreator">Factory for creating <see cref="ICardHandViewModel"/>.</param>
		/// <param name="blackJackButtonViewModelCreator">Factory for creating <see cref="IButtonViewModel"/>.</param>
		/// <param name="playerRound">Hadles the players turn.</param>
		/// <returns>A fully configured <see cref="PlayerViewModel"/> instance.</returns>
		public override IPlayerViewModel CreatePlayerViewModel(	IPlayer<Bitmap, string> player, 
																Subject<SplitSuccessfulEvent> splitSuccessfulEvent, 
																Subject<BetUpdateEvent> betUpdateEvent,
																BlackJackCardHandViewModelCreator blackJackCardHandViewModelCreator,
																BlackJackButtonViewModelCreator blackJackButtonViewModelCreator,
																IPlayerRound<Bitmap, string> playerRound	)
		{
			return new PlayerViewModel(	player, 
										splitSuccessfulEvent, 
										betUpdateEvent, 
										blackJackCardHandViewModelCreator, 
										blackJackButtonViewModelCreator, 
										playerRound);
		}
	}
}
