// Project: BlackJackV2
// file: BlackJackV2/ViewModels/CardHandViewModel.cs

/// <summary>
/// 
///		This class is used to represent a single hand in a view
///		
///		HandOwners.HandOwner		_id				: The id of the hand (primary or split) 
///		int							_bet			: The bet amount for the hand 
///		bool						_handIsActive	: True if the hand is active
///		string						_handValue		: The current value of the hand
///		ObservableCollection<ICard> _cards			: The list of cards in the hand
///
/// </summary>

using System;
using System.Collections.ObjectModel;
using System.Reactive;
using Avalonia.Media.Imaging;
using BlackJackV2.Constants;
using BlackJackV2.Models;
using BlackJackV2.Models.CardHand;
using BlackJackV2.Services.Messaging;
using ReactiveUI;

namespace BlackJackV2.ViewModels
{
	public class CardHandViewModel : ReactiveObject
	{
		private HandOwners.HandOwner _id;
		private int _bet;
		private bool _handIsActive; 
		private string _handValue;
		private ObservableCollection<ICard<Bitmap, string>> _cards;
		
		public HandOwners.HandOwner Id => _id;

		public int Bet
		{
			get => _bet;
			set => this.RaiseAndSetIfChanged(ref _bet, value);
		}

		public bool HandIsActive
		{
			get => _handIsActive;
			set => this.RaiseAndSetIfChanged(ref _handIsActive, value);
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

		public CardHandViewModel(IBlackJackCardHand<Bitmap, string> cardHand)
		{
			_id = cardHand.Id;
			HandIsActive = cardHand.IsActive;
			Bet = 0;
			_cards = cardHand.Hand;
			_handValue = cardHand.HandValue.ToString(); 

			// Update the hand value whenever the cards change
			_cards.CollectionChanged += (sender, e) => 
				HandValue = cardHand.HandValue.ToString();
	
			cardHand.WhenAnyValue(x => x.IsActive)
				.Subscribe ( isActive =>
				{
					HandIsActive = isActive;
				});
		

			// Subscribe to the CardMarkedMessage and update the MarkedCardValue
			CardClickedCommand = ReactiveCommand.Create<string>(markedCardValue =>
			{
				MessageBus.Current.SendMessage(new CardMarkedMessage(markedCardValue));
			});
		}
	}
}
