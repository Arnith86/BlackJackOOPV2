// Project: BlackJackV2
// file: BlackJackV2/Models/GameLogic/CardServices/CardServices.cs

using BlackJackV2.Factories.CardDeckFactory;
using BlackJackV2.Factories.CardHandFactory;
using BlackJackV2.Factories.PlayerHandsFactory;
using BlackJackV2.Models.CardDeck;
using BlackJackV2.Models.CardHand;
using BlackJackV2.Models.GameLogic.CardServices;
using BlackJackV2.Models.PlayerHands;
using BlackJackV2.Shared.Constants;

namespace BlackJackV2.Models.GameLogic.CoreServices
{
	/// <summary>
	/// Provides a centralized service for creating and managing card-related components in the Blackjack game,
	/// including card decks, individual card hands, and collections of player hands.
	/// </summary>
	public class CardServices<TImage, TValue> : ICardServices<TImage, TValue>
	{
		private readonly ICardDeck<TImage, TValue> _cardDeck;
		private readonly BlackJackCardDeckCreator<TImage, TValue> _cardDeckCreator;
		private readonly BlackJackCardHandCreator<TImage, TValue> _cardHandCreator;
		private readonly BlackJackPlayerHandsCreator<TImage, TValue> _playerCardHandsCreator;

		/// <summary>
		/// Initializes a new instance of the <see cref="CardServices{TImage, TValue}"/> class,
		/// setting up factories for deck, hand, and player hand creation, and creating an initial card deck.
		/// </summary>
		/// <param name="cardDeckCreator">Factory responsible for creating a Blackjack card deck.</param>
		/// <param name="cardHandCreator">Factory responsible for creating individual Blackjack card hands.</param>
		/// <param name="playerHandsCreator">Factory responsible for creating collections of player hands.</param>
		public CardServices(BlackJackCardDeckCreator<TImage, TValue> cardDeckCreator,
							BlackJackCardHandCreator<TImage, TValue> cardHandCreator,
							BlackJackPlayerHandsCreator<TImage, TValue> playerHandsCreator)
		{
			_cardDeckCreator = cardDeckCreator;
			_cardHandCreator = cardHandCreator;
			_playerCardHandsCreator = playerHandsCreator;

			_cardDeck = _cardDeckCreator.CreateDeck();
		}

		/// </inheritdoc>
		public ICardDeck<TImage, TValue> CardDeck 
			=> _cardDeck;
		
		/// <inheritdoc/>
		public IBlackJackCardHand<TImage, TValue> GetACardHand(HandOwners.HandOwner whichHand) 
			=> _cardHandCreator.CreateCardHand();
		
		/// <inheritdoc/>
		public IBlackJackPlayerHands<TImage, TValue> GetNewPlayerHands(HandOwners.HandOwner playerID) 
			=>	_playerCardHandsCreator.CreatePlayerHands(playerID, this);
	}
}
