// Project BlackJackV2
// file: BlackJackV2/Factories/ButtonViewModelFactory/ButtonViewModelCreatorBase.cs

using Avalonia.Media.Imaging;
using BlackJackV2.Models.GameLogic.PlayerServices;
using BlackJackV2.Models.Player;
using BlackJackV2.Shared.Constants;
using BlackJackV2.ViewModels.Interfaces;

namespace BlackJackV2.Factories.ViewModelFactories.ButtonViewModelFactory
{
	/// <summary>
	/// Abstract factory base class for creating <see cref="IButtonViewModel"/> instances
	/// using a given <see cref="IPlayerRound{TImage, TValue}"/> context.
	/// </summary>
	/// <remarks>
	/// Related files <see cref="ViewModels.Interfaces"/>
	/// </remarks>
	/// <typeparam name="TImage">The type representing card images.</typeparam>
	/// <typeparam name="TValue">The type representing card values.</typeparam>
	public abstract class ButtonViewModelCreatorBase<TImage, TValue>
	{
		/// <summary>
		/// Creates a new instance of <see cref="IButtonViewModel"/> for the specified player round.
		/// </summary>
		/// <param name="player">The name of the player this set of inputs are linked to.</param>
		/// <param name="primaryOrSplit">Indicates whether the hand is primary or a split hand.</param>
		/// <param name="playerRound">The current player round context.</param>
		/// <returns>A new instance of <see cref="IButtonViewModel"/>.</returns>
		public abstract IButtonViewModel CreateButtonViewModel(
			IPlayer<Bitmap, string> player, 
			HandOwners.HandOwner primaryOrSplit, 
			IPlayerRound<TImage, TValue> playerRound);
	}
}
