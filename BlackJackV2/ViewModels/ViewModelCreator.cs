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
	public class ViewModelCreator
	{
		public static CardHandViewModel CreateHandCardViewModel(/*List<ICard<Bitmap, Bitmap, string>>*/  ObservableCollection<ICard<Bitmap, Bitmap, string>> cardHand)
		{
			return new CardHandViewModel(cardHand);
		}
		public static ButtonViewModel CreateButtonViewModel(GameLogic gameLogic)
		{
			return new ButtonViewModel(gameLogic);
		}
	}
}
