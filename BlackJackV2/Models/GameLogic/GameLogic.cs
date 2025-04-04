using Avalonia.Media.Imaging;
using BlackJackV2.Models.CardDeck;
using BlackJackV2.Models.CardFactory;
using BlackJackV2.Models.CardHand;
using BlackJackV2.Models.Player;
using BlackJackV2.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackV2.Models.GameLogic
{
	public class GameLogic
	{
		PlayerAction playerAction;
		DealerLogic dealerLogic; 

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

			// Here for testing reasons
			blackJackCardDeck.ShuffleDeck();
			StartNewRound(); 
		}

		public void AddCard()
		{
			PlayerCardHand.PrimaryCardHand.AddCard(blackJackCardDeck.GetTopCard());
			PlayerCardHand.SplitCardHand.AddCard(blackJackCardDeck.GetTopCard());
			DealerCardHand.PrimaryCardHand.AddCard(blackJackCardDeck.GetTopCard());
		}

		public void StartNewRound()
		{
			dealerLogic.InitialDeal(DealerCardHand, blackJackCardDeck);
		}
	}
}
