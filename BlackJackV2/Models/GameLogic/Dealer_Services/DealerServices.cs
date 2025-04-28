// Project: BlackJackV2
// file: BlackJackV2/Models/GameLogic/Dealer_Services/DealerLogic.cs

using BlackJackV2.Models.CardDeck;
using BlackJackV2.Models.Card;
using BlackJackV2.Models.CardHand;
using BlackJackV2.Models.PlayerHands;
using BlackJackV2.Factories.PlayerHandsFactory;
using BlackJackV2.Shared.Constants;
using BlackJackV2.Factories.CardHandFactory;

namespace BlackJackV2.Models.GameLogic.Dealer_Services
{
	/// <summary>
	/// This class handles dealer specific services in a blackjack game. 
	/// Including the initial deal and the dealer's logic during their turn.
	/// The dealer only uses the <see cref="IBlackJackPlayerHands{TImage, TValue}.PrimaryCardHand"/>.
	/// </summary>
	public class DealerServices<TImage, TValue> : IDealerServices<TImage, TValue>
	{
		/// <inheritdoc/>
		public IBlackJackPlayerHands<TImage, TValue> DealerCardHand { get => _dealerCardHand; }
		private IBlackJackPlayerHands<TImage, TValue> _dealerCardHand;

		/// <summary>
		/// Initializes a new instance of the <see cref="DealerServices{TImage, TValue}"/> class.
		/// Creates the hand associated with the dealer.
		/// </summary>
		/// <param name="playerHandsCreator">Creates the playerHands container.</param>
		/// <param name="cardHandCreator">Creates the instances of <see cref="IBlackJackCardHand{TImage, TValue}"/> used in the <see cref="PlayerHands"/> container.</param>
		public DealerServices(BlackJackPlayerHandsCreator<TImage, TValue> playerHandsCreator, BlackJackCardHandCreator<TImage, TValue> cardHandCreator)
		{
			_dealerCardHand = playerHandsCreator.CreatePlayerHands(HandOwners.HandOwner.Dealer, cardHandCreator);
		}

		///<inheritdoc/>
		public void InitialDeal(IBlackJackPlayerHands<TImage, TValue> dealerHands, ICardDeck<TImage, TValue> cardDeck)
		{
			ICard<TImage, TValue> firstCard = cardDeck.GetTopCard();
			firstCard.FlipCard();
			dealerHands.PrimaryCardHand.AddCard(firstCard);
			dealerHands.PrimaryCardHand.AddCard(cardDeck.GetTopCard());
		}

		///<inheritdoc/>
		public void DealerFinishTurn(IBlackJackPlayerHands<TImage, TValue> dealerHands, ICardDeck<TImage, TValue> cardDeck)
		{
			dealerHands.PrimaryCardHand.Hand[0].FlipCard();
			dealerHands.PrimaryCardHand.RecalculateHandAfterCardFlip();

			if (dealerHands.PrimaryCardHand.HandValue <= 17)
				DrawUntillSeventeen(dealerHands, cardDeck);
		}

		// Dealer must hit until they reach a total of 17 or higher
		private void DrawUntillSeventeen(IBlackJackPlayerHands<TImage, TValue> dealerHands, ICardDeck<TImage, TValue> cardDeck)
		{
			while (dealerHands.PrimaryCardHand.HandValue < 17)
				dealerHands.PrimaryCardHand.AddCard(cardDeck.GetTopCard());
		}
	}
}
