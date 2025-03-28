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

namespace BlackJackV2.Models
{
	public class GameLogic
	{
		// Used to create a deck of cards
		private BlackJackCardDeckCreator blackJackCardDeckCreator = new BlackJackCardDeckCreator();
		private ICardDeck<Bitmap, Bitmap, string> blackJackCardDeck;
		
		// Represents the player and dealer hands
		IPlayerHands<Bitmap, Bitmap, string> _playerCardHand;
		IPlayerHands<Bitmap, Bitmap, string> _dealerCardHand;

		public IPlayerHands<Bitmap, Bitmap, string> PlayerCardHand { get => _playerCardHand; }
		public IPlayerHands<Bitmap, Bitmap, string> DealerCardHand { get => _dealerCardHand; }

		public GameLogic()
		{
			blackJackCardDeck = blackJackCardDeckCreator.CreateBlackJackCardDeck();
			_playerCardHand = BlackJackPlayerHandsCreator.CreateBlackJackPlayerHand();
			_dealerCardHand = BlackJackPlayerHandsCreator.CreateBlackJackPlayerHand();

			blackJackCardDeck.ShuffleDeck();
			//playerCardHand.AddCard(blackJackCardDeck.GetTopCard());
			//int temp = playerCardHand.HandValue;
			//playerCardHand.AddCard(blackJackCardDeck.GetTopCard());
			//temp = playerCardHand.HandValue;
			//playerCardHand.AddCard(blackJackCardDeck.GetTopCard());
			//temp = playerCardHand.HandValue;
			//playerCardHand.AddCard(blackJackCardDeck.GetTopCard());
			//temp = playerCardHand.HandValue;
			//cardHand.AddCard(blackJackCardDeck.GetTopCard());
			//temp = cardHand.HandValue;

			//IPlayerHand<Bitmap, Bitmap, string> playerHand = new PlayerHand(playerCardHand);
			PlayerAction playerAction = new PlayerAction();
			playerAction.Split("Clubs_1", _playerCardHand);

			// TestImage = cardHand.Hand[0].FrontImage;
		}

		public void AddCard(/*ICardHand<Bitmap, Bitmap, string> cardHand, BlackJackCardDeck blackJackCardDeck*/)
		{
			Console.WriteLine("KAKAKA");
			//cardHand.AddCard(blackJackCardDeck.GetTopCard());
		}
	}
}
