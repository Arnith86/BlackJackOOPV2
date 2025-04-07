using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using BlackJackV2.Models;
using BlackJackV2.Models.CardFactory;
using BlackJackV2.Models.CardHand;
using ReactiveUI;

namespace BlackJackV2.ViewModels
{
	/**
	 * This class is used to represent the player's hand in the view
	 **/

	public class CardHandViewModel : ReactiveObject
	{
		private string _id;
		private string _handValue;
		private ObservableCollection<ICard<Bitmap, string>> _cards;

		public string Id => _id;
		public string HandValue
		{
			get => _handValue;
			set => this.RaiseAndSetIfChanged(ref _handValue, value);
		}
		
		public ObservableCollection<ICard<Bitmap, string>> Cards
		{
			get => _cards;
			set => this.RaiseAndSetIfChanged(ref _cards, value);
		}

		public CardHandViewModel(string id, ObservableCollection<ICard<Bitmap, string>> cards)
		public ReactiveCommand<string, Unit> CardClickedCommand { get; }

		public CardHandViewModel(string id,  ICardHand<Bitmap, string> cardHand, string handValue)
		{
			_id = id;
			_cards = cards;
			_cards = cardHand.Hand;
			_handValue = cardHand.HandValue.ToString();

			// Update the hand value whenever the cards change
			_cards.CollectionChanged += (sender, e) => 
				HandValue = cardHand.HandValue.ToString();
	
			
		}
	}
}
