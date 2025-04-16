using Avalonia.Media.Imaging;
using ReactiveUI;
using BlackJackV2.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackV2.Models.Player
{
	/**
	 * This class represents a player in the game.
	 * The player will start with a set amount of money and can place bets on their hands.
	 * 
	 * string Name 				: The name of the player
	 * int Funds				: The amount of money the player has
	 * IPlayerHands PlayerHands : The player's hands in the game
	 * 
	 * PlaceBet()			: Places a bet for the player for the specified hand
	 * PayOut()				: Add specified amount to the player funds
	 * 
	 **/

	public class Player : ReactiveObject, IPlayer
	{
		public string Name { get; private set; }
		
		private int _funds = 10;
		public int Funds 
		{ 
			get => _funds;
			set => this.RaiseAndSetIfChanged(ref _funds, value);
		} 


		public IPlayerHands<Bitmap, string> hands { get; }

		public Player(string name, IPlayerHands<Bitmap, string> hands)
		{
			Name = name;
			this.hands = hands;
		}

		public bool PlaceBet(HandOwners.HandOwner owner, int amount)
		{
			if (Funds >= amount)
			{
				hands.SetBetToHand(owner, amount);
				Funds -= amount;
				return true;
			}
			return false; 
		}
		public void PayOut(int amount)
		{
			Funds += amount;
		}
	}
}
