using Avalonia.Media.Imaging;
using BlackJackV2.Models;
using BlackJackV2.Models.CardHand;
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
		public static CardHandViewModel CreateHandCardViewModel(string id,  ObservableCollection<ICard<Bitmap, string>> cardHand)
		{
			return new CardHandViewModel(id, cardHand);
		}
		public static ButtonViewModel CreateButtonViewModel(GameLogic gameLogic)
		{
			return new ButtonViewModel(gameLogic);
		}

		public static TableViewModel CreateTableViewModel(GameLogic gameLogic)
		{
			return new TableViewModel(gameLogic);
		}
	}
}
