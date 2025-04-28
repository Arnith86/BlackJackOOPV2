// Project: BlackJackV2
// file: BlackJackV2/Models/Player/Player.cs

using ReactiveUI;
using BlackJackV2.Services.Events;
using System.Reactive.Subjects;
using BlackJackV2.Models.PlayerHands;
using BlackJackV2.Shared.Constants;

namespace BlackJackV2.Models.Player
{
	/// <summary>
	/// This class represents a player in a blackjack game.
	///	The player will start with a set amount of funds and can place bets on their hands.
	/// </summary>
	public class Player<TImage, TValue> : ReactiveObject, IPlayer<TImage, TValue>
	{
		/// <inheritdoc/>
		public string Name { get; private set; }
		
		private int _funds = 10;
		/// <inheritdoc/>
		public int Funds 
		{ 
			get => _funds;
			set => this.RaiseAndSetIfChanged(ref _funds, value);
		}

		/// <summary>
		/// Gets the wrapper class containing the player's primary and split hands.
		/// </summary>
		public IBlackJackPlayerHands<TImage, TValue> Hands { get; }

		/// <summary>
		///	The subject used to notify when the bet is updated 
		/// </summary>
		private readonly Subject<BetUpdateEvent> _betUpdateSubject;

		/// <summary>
		/// Initializes a new instance of the <see cref="Player"/> class.
		/// </summary>
		/// <param name="name">The player's chosen name.</param>
		/// <param name="hands">The wrapper for the primary and split hands.</param>
		/// <param name="betUpdateSubject">Subject used to notify when a bet has been placed.</param>
		/// <remarks> Related files <see cref="BlackJackV2.Factories.PlayerFactory"/></remarks>
		public Player(string name, IBlackJackPlayerHands<TImage, TValue> hands, Subject<BetUpdateEvent> betUpdateSubject)
		{
			Name = name;
			this.Hands = hands;
			_betUpdateSubject = betUpdateSubject;
		}

		/// <inheritdoc/>
		public bool PlaceBet(HandOwners.HandOwner owner, int amount, bool doubleDown = false)
		{
			if (Funds >= amount)
			{
				// Check if the player is trying to double down
				int totalBet = doubleDown ? amount * 2 : amount; 
				Hands.SetBetToHand(owner, totalBet);
				
				Funds -= amount;
				
				_betUpdateSubject.OnNext(new BetUpdateEvent(Name, owner));
				
				return true;
			}
			return false; 
		}

		/// <inheritdoc/>
		public bool EnoughFundsForBet(int amount)
		{
			return Funds >= amount;
		}

		/// <inheritdoc/>
		public void PayOut(int amount)
		{
			Funds += amount;
		}
	}
}
