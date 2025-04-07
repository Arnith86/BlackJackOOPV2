using Avalonia.Media.Imaging;
using BlackJackV2.Models.CardDeck;
using BlackJackV2.Models.CardFactory;
using BlackJackV2.Models.CardHand;
using BlackJackV2.Models.Player;
using BlackJackV2.ViewModels;
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
		// Possible active hands in a blackjack game
		public enum ActiveHand
		{
			Primary,
			Split,
			Dealer
		}

		// Represents the current active hand in the game
		ActiveHand activeHand = ActiveHand.Primary;

		// Handles the blackjack related actions the players can take
		PlayerAction playerAction;
		// Handles the dealer's turn in a blackjack game
		DealerLogic dealerLogic;
		// Handles the evaluation of the round
		RoundEvaluator roundEvaluator;

		// Used to create a deck of cards
		private BlackJackCardDeck blackJackCardDeck;
		
		// Represents the player and dealer hands
		IPlayerHands<Bitmap, string> _playerCardHand;
		IPlayerHands<Bitmap, string> _dealerCardHand;

		public IPlayerHands<Bitmap, string> PlayerCardHand { get => _playerCardHand; }
		public IPlayerHands<Bitmap, string> DealerCardHand { get => _dealerCardHand; }

		public GameLogic()
		{
			blackJackCardDeck = (BlackJackCardDeck) BlackJackCardDeckCreator.CreateBlackJackCardDeck();
			_playerCardHand = BlackJackPlayerHandsCreator.CreateBlackJackPlayerHand();
			_dealerCardHand = BlackJackPlayerHandsCreator.CreateBlackJackPlayerHand();

			playerAction = GameLogicCreator.CreatePlayerAction();
			dealerLogic = GameLogicCreator.CreateDealerLogic();
			roundEvaluator = GameLogicCreator.CreateRoundEvaluator();

			// Here for testing reasons
			blackJackCardDeck.ShuffleDeck();
			StartNewRound();
			
		}

		public void HitAction()
		{
			if (activeHand == ActiveHand.Primary) playerAction.Hit(PlayerCardHand.PrimaryCardHand, blackJackCardDeck);
			else if (activeHand == ActiveHand.Split) playerAction.Hit(PlayerCardHand.SplitCardHand, blackJackCardDeck);
			//PlayerCardHand.PrimaryCardHand.AddCard(blackJackCardDeck.GetTopCard());
			//PlayerCardHand.SplitCardHand.AddCard(blackJackCardDeck.GetTopCard());
		}

		public void StartNewRound()
		{
			dealerLogic.InitialDeal(DealerCardHand, blackJackCardDeck);
		}
		public void FinishRound()
		{
			dealerLogic.DealerFinishTurn(DealerCardHand, blackJackCardDeck);
		}

		public void Fold()
		{

		}

		public void EvaluateRound()
		{
			Debug.WriteLine( roundEvaluator.EvaluateRound(PlayerCardHand.PrimaryCardHand, DealerCardHand.PrimaryCardHand));
		}

		// Validates and initiates procedure for splitting the hand
		// Only on the initial deal, in primary hand
		public bool SplitAction() => activeHand == ActiveHand.Primary && playerAction.Split(PlayerCardHand);
	}
}
