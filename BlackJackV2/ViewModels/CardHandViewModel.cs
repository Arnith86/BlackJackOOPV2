using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using BlackJackV2.Constants;
using BlackJackV2.Models;
using BlackJackV2.Models.CardFactory;
using BlackJackV2.Models.CardHand;
using BlackJackV2.Services.Messaging;
using ReactiveUI;
using ReactiveUI.Legacy;

namespace BlackJackV2.ViewModels
{
	/**
	 * This class is used to represent the player's hand in the view
	 **/

	public class CardHandViewModel : ReactiveObject
	{
		private HandOwners.HandOwner _id;
		private string _markedCardValue;
		private string _handValue;
		private ObservableCollection<ICard<Bitmap, string>> _cards;
		
		public HandOwners.HandOwner Id => _id;
		
		public string MarkedCardValue
		{
			get => _markedCardValue;
			set => this.RaiseAndSetIfChanged(ref _markedCardValue, value);
		}

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

		public ReactiveCommand<string, Unit> CardClickedCommand { get; }

		public CardHandViewModel(HandOwners.HandOwner id, IBlackJackCardHand<Bitmap, string> cardHand, string handValue)
		{
			_id = id;
			_cards = cardHand.Hand;
			_handValue = cardHand.HandValue.ToString();

			// Update the hand value whenever the cards change
			_cards.CollectionChanged += (sender, e) => 
				HandValue = cardHand.HandValue.ToString();
	
			
			// Subscribe to the CardMarkedMessage and update the MarkedCardValue
			CardClickedCommand = ReactiveCommand.Create<string>(markedCardValue =>
			{
				MessageBus.Current.SendMessage(new CardMarkedMessage(markedCardValue));
			});

			

			//// Subscribe to each card in the hand for property changes
			//foreach (ICard<Bitmap, string> card in _cards)
			//{
			//	SubscribeToCard((BlackJackCard)card);
			//}
		}

		//private void SubscribeToCard(BlackJackCard card)
		//{
		//	card.PropertyChanged += Card_PropertyChanged;
		//}

		//private void Card_PropertyChanged(object? sender, PropertyChangedEventArgs e)
		//{
		//	if (e.PropertyName == nameof(ICard<Bitmap, string>.FaceDown)) 
		//	{
		//		Debug.WriteLine($"Card's FaceDown value changed: {((ICard<Bitmap, string>)sender!).Value}");
		//	}
		//}
	}
}
