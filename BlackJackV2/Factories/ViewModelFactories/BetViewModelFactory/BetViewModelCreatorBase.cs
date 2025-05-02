// Project BlackJackV2
// file: BlackJackV2/Factories/BetViewModelFactory/BetViewModelCreatorBase.cs

using Avalonia.Media.Imaging;
using BlackJackV2.Models.GameLogic.GameRuleServices.Interfaces;
using BlackJackV2.Models.GameLogic.PlayerServices;
using BlackJackV2.Models.Player;
using BlackJackV2.ViewModels.Interfaces;

namespace BlackJackV2.Factories.ViewModelFactories.BetViewModelFactory
{
	/// <summary>
	/// Base class for creating instances of <see cref="IBetViewModel"/>.
	/// Provides an abstraction for factories that generate bet view models for different contexts or configurations.
	/// </summary>
	/// <remarks>
	/// Related files <see cref="ViewModels.Interfaces"/>
	/// </remarks>
	public abstract class BetViewModelCreatorBase
	{
		/// <summary>
		/// Creates and returns a new instance of a class implementing <see cref="IBetViewModel"/>.
		/// </summary>
		/// <param name="player">Player placing the bet.</param>
		/// <param name="playerServices">Service for sending the bet to the hand.</param>
		/// <param name="gameRule">Game rules to validate the bet.</param>
		/// <returns>An instance of <see cref="IBetViewModel"/>.</returns>
		public abstract IBetViewModel CreateBetViewModel(
			IPlayer<Bitmap, string> player,
			IPlayerServices<Bitmap, string> playerServices,
			IGameRules<Bitmap, string> gameRule);
	}
}
