// Project: BlackJackV2
// file: BlackJackV2/Models/Player/Player.cs

/// <summary>
/// 
///		 This class represents a player in the game.
///		 The player will start with a set amount of money and can place bets on their hands.
///		 
///		string			Name		: The name of the player
///		int				Funds		: The amount of money the player has
///		IPlayerHands	PlayerHands : The player's hands in the game
///		
///		bool	PlaceBet()	: Places a bet for the player for the specified hand
///		void	PayOut()	: Add specified amount to the player funds
///		
/// </summary>

using Avalonia.Media.Imaging;
using ReactiveUI;
using BlackJackV2.Constants;
using BlackJackV2.Services.Events;
using System.Reactive.Subjects;
using BlackJackV2.Models.PlayerHands;

namespace BlackJackV2.Models.Player
{
	public class Player : ReactiveObject, IPlayer
	{
		public string Name { get; private set; }
		
		private int _funds = 10;
		public int Funds 
		{ 
			get => _funds;
			set => this.RaiseAndSetIfChanged(ref _funds, value);
		} 

		public IBlackJackPlayerHands<Bitmap, string> hands { get; }

		// The subject used to notify when the bet is updated
		/*private*/
		public ISubject<BetUpdateEvent> _betUpdateSubject;

		public Player(string name, IBlackJackPlayerHands<Bitmap, string> hands, ISubject<BetUpdateEvent> betUpdateSubject)
		{
			Name = name;
			this.hands = hands;
			_betUpdateSubject = betUpdateSubject;
		}

		public bool PlaceBet(HandOwners.HandOwner owner, int amount, bool doubleDown = false)
		{
			if (Funds >= amount)
			{
				// Check if the player is trying to double down
				int totalBet = doubleDown ? amount * 2 : amount; 

				hands.SetBetToHand(owner, totalBet);
				Funds -= amount;
				_betUpdateSubject.OnNext(new BetUpdateEvent(Name, owner));
				return true;
			}
			return false; 
		}

		public bool EnoughFundsForBet(int amount)
		{
			return Funds >= amount;
		}

		public void PayOut(int amount)
		{
			Funds += amount;
		}
	}
}
