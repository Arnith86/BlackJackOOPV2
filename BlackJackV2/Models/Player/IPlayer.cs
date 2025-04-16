using Avalonia.Media.Imaging;
using BlackJackV2.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackV2.Models.Player
{
	/**
	 * Interface for Player specific handeling 
	 **/
	public interface IPlayer
	{
		public string Name { get; }
		int Funds { get; }
		public IPlayerHands<Bitmap, string> hands { get; }

		public bool PlaceBet(HandOwners.HandOwner owner, int amount);
		public void PayOut(int amount);

	}
}
