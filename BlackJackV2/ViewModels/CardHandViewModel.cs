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
	/**
	 * This class is used to represent the player's hand in the view
	 **/

	public class CardHandViewModel : ReactiveObject
	{
		private string _id;
		private ObservableCollection<ICard<Bitmap, string>> _cards;

		public string Id => _id;
		public ObservableCollection<ICard<Bitmap, string>> Cards
		{
			get => _cards;
			set => this.RaiseAndSetIfChanged(ref _cards, value);
		}

		public CardHandViewModel(string id, ObservableCollection<ICard<Bitmap, string>> cards)
		{
			_id = id;
			_cards = cards;
		}
	}
}
