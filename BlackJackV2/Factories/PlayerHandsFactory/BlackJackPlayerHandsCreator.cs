// Project BlackJackV2
// file: BlackJackV2/Factories/PlayerHandsFactory/BlackJackPlayerHandsCreator.cs

using BlackJackV2.Factories.CardHandFactory;
using BlackJackV2.Models.PlayerHands;
using BlackJackV2.Shared.Constants;

namespace BlackJackV2.Factories.PlayerHandsFactory
{
	/// <summary>
	/// Concrete creator class for the Player Hands factory pattern, responsible for creating fully initialized <see cref="BlackJackPlayerHands"/> objects.
	/// This class instantiates a structure containing one or more card hands associated with a specific participant in the Blackjack game.
	/// </summary>
	/// <remarks> Related files <see cref="BlackJackV2.Models.PlayerHands"/></remarks>
	public class BlackJackPlayerHandsCreator<TImage, TValue> : PlayerHandsCreator<TImage, TValue>
	{
		/// <summary>
		/// Creates a new player hands object associated with a specific game participant (player or dealer),
		/// using the provided <see cref="CardHandCreator{TImage, TValue}"/> to generate individual hands.
		/// </summary>
		/// <param name="id">
		/// Identifies the owner of the hands being created using the <see cref="HandOwners.HandOwner"/> enum.
		/// </param>
		/// <param name="cardHandCreator">
		/// Factory used to generate the underlying card hand(s) that make up the player's hand structure.
		/// </param>
		/// <returns>
		/// A fully initialized <see cref="BlackJackPlayerHands"/> instance representing the player or dealer's hands.
		/// </returns>
		public override IBlackJackPlayerHands<TImage, TValue> CreatePlayerHands(HandOwners.HandOwner id, CardHandCreator<TImage, TValue> cardHandCreator)
		{
			return new BlackJackPlayerHands<TImage, TValue>(id, cardHandCreator);
		}
	}
}
