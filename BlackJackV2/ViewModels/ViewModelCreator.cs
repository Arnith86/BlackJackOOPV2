using Avalonia.Media.Imaging;
using BlackJackV2.Constants;
using BlackJackV2.Models;
using BlackJackV2.Models.CardHand;
using BlackJackV2.Models.GameLogic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackV2.ViewModels
{
	/**
	 * This class is used to create the view models for the game
	 **/
	public class ViewModelCreator
	{
		public static CardHandViewModel CreateHandCardViewModel(HandOwners.HandOwner id, IBlackJackCardHand<Bitmap, string> cardHand, string handValue)
		{
			return new CardHandViewModel(id, cardHand, handValue);
		}

		public static StatsViewModel CreateStatsViewModel(GameLogic gameLogic)
		{
			return new StatsViewModel(gameLogic);
		}

		public static ButtonViewModel CreateButtonViewModel(PlayerRound playerRound)
		{
			return new ButtonViewModel(playerRound);
		}

		public static TableViewModel CreateTableViewModel(GameLogic gameLogic)
		{
			return new TableViewModel(gameLogic);
		}
	}
}
