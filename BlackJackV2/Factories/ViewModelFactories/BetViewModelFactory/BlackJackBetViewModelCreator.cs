// Project BlackJackV2
// file: BlackJackV2/Factories/BetViewModelFactory/BlackJackBetViewModelCreator.cs

using Avalonia.Media.Imaging;
using BlackJackV2.ViewModels;
using BlackJackV2.Models.GameLogic.GameRuleServices.Interfaces;
using BlackJackV2.Models.GameLogic.PlayerServices;
using BlackJackV2.Models.Player;
using BlackJackV2.ViewModels.Interfaces;

namespace BlackJackV2.Factories.ViewModelFactories.BetViewModelFactory
{
	/// <summary>
	/// Concrete implementation of <see cref="BetViewModelCreatorBase"/> that creates instances of <see cref="BetViewModel"/>
	/// for use in a Blackjack game.
	/// </summary>
	/// <remarks>
	/// Related files <see cref="ViewModels.Interfaces"/>
	/// </remarks>
	public class BlackJackBetViewModelCreator : BetViewModelCreatorBase
	{
		/// <summary>
		/// Creates a new instance of <see cref="BetViewModel"/> using the specified player, player services, and game rules.
		/// </summary>
		/// <param name="player">The player placing the bet.</param>
		/// <param name="playerServices">The player service handler.</param>
		/// <param name="gameRule">The game rule logic to validate the bet.</param>
		/// <returns>An instance of <see cref="IBetViewModel"/> initialized for the given player.</returns>
		public override IBetViewModel CreateBetViewModel(
			IPlayer<Bitmap, string> player,
			IPlayerServices<Bitmap, string> playerServices,
			IGameRules<Bitmap, string> gameRule)
		{
			return new BetViewModel(player, playerServices,gameRule);
		}
	}
}
