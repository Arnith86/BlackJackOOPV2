using Avalonia.Media.Imaging;
using BlackJackV2.Constants;
using BlackJackV2.Models.CardDeck;
using BlackJackV2.Models.CardFactory;
using BlackJackV2.Models.CardHand;
using BlackJackV2.Models.Player;
using BlackJackV2.ViewModels;
using BlackJackV2.Services.Messaging;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackV2.Models.GameLogic
{
	public class GameLogic
	{
		// Represents the current active hand in the game
		HandOwners.HandOwner activeHand;


		// Used to create a deck of cards
		private BlackJackCardDeck blackJackCardDeck;
		// Handles the blackjack related actions the players can take
		private PlayerAction playerAction;
		// Handles the dealer's turn in a blackjack game
		private DealerLogic dealerLogic;
		// Handles the evaluation of the round
		private RoundEvaluator roundEvaluator;
		// Handles all rounds related to a players hands
		public PlayerRound playerRound;

		// Represents the player and dealer hands
		IPlayerHands<Bitmap, string> _playerCardHand;
		IPlayerHands<Bitmap, string> _dealerCardHand;

		public IPlayerHands<Bitmap, string> PlayerCardHand { get => _playerCardHand; }
		public IPlayerHands<Bitmap, string> DealerCardHand { get => _dealerCardHand; }

		public GameLogic()
		{
			blackJackCardDeck = (BlackJackCardDeck) BlackJackCardDeckCreator.CreateBlackJackCardDeck();
			_playerCardHand = BlackJackPlayerHandsCreator.CreateBlackJackPlayerHand(HandOwners.HandOwner.Player);
			_dealerCardHand = BlackJackPlayerHandsCreator.CreateBlackJackPlayerHand(HandOwners.HandOwner.Dealer);

			playerAction = GameLogicCreator.CreatePlayerAction();
			dealerLogic = GameLogicCreator.CreateDealerLogic();
			roundEvaluator = GameLogicCreator.CreateRoundEvaluator();
			playerRound = GameLogicCreator.CreatePlayerRound(blackJackCardDeck, playerAction);
			activeHand = HandOwners.HandOwner.Primary;

			// Here for testing reasons
			blackJackCardDeck.ShuffleDeck();
		
			MessageBus.Current.SendMessage(new ActiveHandMessage(activeHand));
		}

		
		public async void StartNewRound()
		{
			dealerLogic.InitialDeal(DealerCardHand, blackJackCardDeck);
			
			await playerRound.PlayerTurn((PlayerHands)_playerCardHand); 
			// These will be removed after testing finishes
			dealerLogic.DealerFinishTurn(DealerCardHand, blackJackCardDeck);
			EvaluateRound();
		}

		public void FinishRound()
		{
			dealerLogic.DealerFinishTurn(DealerCardHand, blackJackCardDeck);
		}

		public void EvaluateRound()
		{
			Debug.WriteLine(roundEvaluator.EvaluateRound(PlayerCardHand.PrimaryCardHand, DealerCardHand.PrimaryCardHand));
		}
	}
}
