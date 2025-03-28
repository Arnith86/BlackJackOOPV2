using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using BlackJackV2.Models;
using BlackJackV2.Models.CardFactory;
using ReactiveUI;

namespace BlackJackV2.ViewModels
{
	public class CardHandViewModel : ReactiveObject
	{
		//private List<ICard<Bitmap, Bitmap, string>> _cards;
		//public List<ICard<Bitmap, Bitmap, string>> Cards =>  _cards;

		private ObservableCollection<ICard<Bitmap, Bitmap, string>> _cards;
		public ObservableCollection<ICard<Bitmap, Bitmap, string>> Cards => _cards;

		public CardHandViewModel(ObservableCollection<ICard<Bitmap, Bitmap, string>>/*List<ICard<Bitmap, Bitmap, string>>*/ cards) 
		{
			_cards = cards;
		}
	}
}
