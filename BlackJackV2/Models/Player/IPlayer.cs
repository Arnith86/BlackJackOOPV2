// Project: BlackJackV2
// file: BlackJackV2/Models/Player/IPlayer.cs

/// <summary>
///		Interface for Player specific handeling 
///		
///		string			Name	: Player name 
///		int				Funds	: Players current found amount 
///		IPlayerHands<>	hands	: Object containing the players hands
///		
///		bool			PlaceBet(HandOwners.HandOwner, int)	: Places a bet for the player for the specified hand
///		void			PayOut(int amount)					: Add specified amount to the player funds			
/// </summary>

using Avalonia.Media.Imaging;
using BlackJackV2.Constants;
using BlackJackV2.Models.PlayerHands;

namespace BlackJackV2.Models.Player
{
	public interface IPlayer
	{
		public string Name { get; }
		public int Funds { get; }
		public IBlackJackPlayerHands<Bitmap, string> hands { get; }

		public bool PlaceBet(HandOwners.HandOwner owner, int amount);
		public void PayOut(int amount);

	}
}
