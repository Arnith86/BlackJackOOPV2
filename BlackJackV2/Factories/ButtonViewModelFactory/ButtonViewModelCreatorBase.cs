// Project BlackJackV2
// file: BlackJackV2/Factories/ButtonViewModelFactory/ButtonViewModelCreatorBase.cs

using BlackJackV2.Models.GameLogic.PlayerServices;
using BlackJackV2.Shared.Constants;
using BlackJackV2.ViewModels.Interfaces;

namespace BlackJackV2.Factories.ButtonViewModelFactory
{
	/// <summary>
	/// Abstract factory base class for creating <see cref="IButtonViewModel"/> instances
	/// using a given <see cref="IPlayerRound{TImage, TValue}"/> context.
	/// </summary>
	/// <typeparam name="TImage">The type representing card images.</typeparam>
	/// <typeparam name="TValue">The type representing card values.</typeparam>
	public abstract class ButtonViewModelCreatorBase<TImage, TValue>
	{
		/// <summary>
		/// Creates a new instance of <see cref="IButtonViewModel"/> for the specified player round.
		/// </summary>
		/// <param name="playerRound">The round context from which to generate the view model.</param>
		/// <returns>A new instance of <see cref="IButtonViewModel"/>.</returns>
		public abstract IButtonViewModel CreateButtonViewModel(string playerName, HandOwners.HandOwner primaryOrSplit, IPlayerRound<TImage, TValue> playerRound);
	}
}
