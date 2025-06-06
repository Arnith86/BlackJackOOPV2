﻿// Project: BlackJackV2
// file: BlackJackV2/Models/GameLogic/Dealer_Services/DealerLogic.cs

using BlackJackV2.Models.CardDeck;
using BlackJackV2.Models.Card;
using BlackJackV2.Models.PlayerHands;
using BlackJackV2.Shared.Constants;
using BlackJackV2.Models.GameLogic.CardServices;
using BlackJackV2.Models.Player;
using BlackJackV2.Factories.PlayerFactory;


namespace BlackJackV2.Models.GameLogic.Dealer_Services
{
	/// <summary>
	/// This class handles dealer specific services in a blackjack game. 
	/// Including the initial deal and the dealer's logic during their turn.
	/// The dealer only uses the <see cref="IBlackJackPlayerHands{TImage, TValue}.PrimaryCardHand"/>.
	/// </summary>
	public class DealerServices<TImage, TValue> : IDealerServices<TImage, TValue>
	{
		private IPlayer<TImage, TValue> _dealer;

		/// <inheritdoc/>
		public IBlackJackPlayerHands<TImage, TValue> DealerCardHand { get => _dealerCardHand; }
		private IBlackJackPlayerHands<TImage, TValue> _dealerCardHand;

		/// <summary>
		/// Initializes a new instance of the <see cref="DealerServices{TImage, TValue}"/> class.
		/// Creates the hand associated with the dealer.
		/// </summary>
		/// <param name="cardServices">Handles card-related services such as creating new cards, decks and hands.</param>
		/// <param name="playerCreator">Handles player creation.</param>
		public DealerServices(ICardServices<TImage, TValue> cardServices, BlackJackPlayerCreator<TImage, TValue> playerCreator)
		{
			_dealerCardHand = cardServices.GetNewPlayerHands(HandOwners.HandOwner.Dealer);

			_dealer = playerCreator.CreatePlayer(
				cardServices.GetNewPlayerHands(HandOwners.HandOwner.Dealer),
				null,
				HandOwners.HandOwner.Dealer.ToString()
			);
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
				DrawUntilSeventeen(dealerHands, cardDeck);
		}

		// Dealer must hit until they reach a total of 17 or higher
		private void DrawUntilSeventeen(IBlackJackPlayerHands<TImage, TValue> dealerHands, ICardDeck<TImage, TValue> cardDeck)
		{
			while (dealerHands.PrimaryCardHand.HandValue < 17)
				dealerHands.PrimaryCardHand.AddCard(cardDeck.GetTopCard());
		}

		/// <inheritdoc/>
		public void ResetDealerCardHand() => 
			_dealerCardHand.ResetHand();
	}
}
