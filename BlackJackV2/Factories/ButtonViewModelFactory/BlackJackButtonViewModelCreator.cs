// Project BlackJackV2
// file: BlackJackV2/Factories/ButtonViewModelFactory/ButtonViewModelCreator.cs

using Avalonia.Media.Imaging;
using BlackJackV2.Models.GameLogic.PlayerServices;
using BlackJackV2.Shared.Constants;
using BlackJackV2.ViewModels;
using BlackJackV2.ViewModels.Interfaces;

namespace BlackJackV2.Factories.ButtonViewModelFactory
{
	/// <summary>
	/// Concrete implementation of <see cref="ButtonViewModelCreatorBase{Bitmap, string}"/> 
	/// for creating <see cref="ButtonViewModel"/> instances using a <see cref="Bitmap"/> image type and <see cref="string"/> value type.
	/// Used in the BlackJack game context.
	/// </summary>
	public class BlackJackButtonViewModelCreator : ButtonViewModelCreatorBase<Bitmap, string>
	{
		/// <summary>
		/// Creates a new <see cref="ButtonViewModel"/> instance using the specified <paramref name="playerRound"/>.
		/// </summary>
		/// <param name="playerRound">The current player round context.</param>
		/// <returns>A new instance of <see cref="IButtonViewModel"/>.</returns>
		public override IButtonViewModel CreateButtonViewModel(string playerName, HandOwners.HandOwner primaryOrSplit, IPlayerRound<Bitmap, string> playerRound)
		{
			return new ButtonViewModel(playerName, primaryOrSplit, playerRound);
		}
	}
}
