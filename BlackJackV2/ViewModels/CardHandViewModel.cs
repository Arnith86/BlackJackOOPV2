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
///		readonly CompositeDisposable _disposables	: Used to clean up resources
///		
///		void Dispose()								: Cleans up resources
///
/// </summary>

using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Disposables;
using Avalonia.Media.Imaging;
using BlackJackV2.Constants;
using BlackJackV2.Models.Card;
using BlackJackV2.Models.CardHand;
using BlackJackV2.Models.GameLogic;
using BlackJackV2.Services.Messaging;
using ReactiveUI;

namespace BlackJackV2.ViewModels
{
	public class CardHandViewModel : ReactiveObject
	{
		private HandOwners.HandOwner _id;
		public HandOwners.HandOwner Id => _id;


		private int _bet;
		public int Bet
		{
			get => _bet;
			set => this.RaiseAndSetIfChanged(ref _bet, value);
		}
		
		private bool _handIsActive;
		public bool HandIsActive
		{
			get => _handIsActive;
			set => this.RaiseAndSetIfChanged(ref _handIsActive, value);
		}
		
		
		private string _handValue;
		public string HandValue
		{
			get => _handValue;
			set => this.RaiseAndSetIfChanged(ref _handValue, value);
		}
		
		private ObservableCollection<ICard<Bitmap, string>> _cards;
		public ObservableCollection<ICard<Bitmap, string>> Cards
		{
			get => _cards;
			set => this.RaiseAndSetIfChanged(ref _cards, value);
		}

		public ReactiveCommand<string, Unit> CardClickedCommand { get; }
		public ButtonViewModel ButtonViewModel { get; }

		private readonly CompositeDisposable _disposables = new CompositeDisposable();

		public CardHandViewModel(IBlackJackCardHand<Bitmap, string> cardHand)
		{
			_id = cardHand.Id;
			HandIsActive = cardHand.IsActive;
			Bet = 0;
			_cards = cardHand.Hand;
			_handValue = cardHand.HandValue.ToString();


			_cards.CollectionChanged += (sender, e) => 
				HandValue = cardHand.HandValue.ToString();
			
			// Automatically update values if the subscribed values change
			cardHand.WhenAnyValue(x => x.IsActive, x => x.HandValue)
				.Subscribe (  tuple =>
				{
					var(isActive, handValue) = tuple;
					HandIsActive = isActive;
					HandValue = handValue.ToString();
				})
				.DisposeWith(_disposables);
		

			// Subscribe to the CardMarkedMessage and update the MarkedCardValue
			CardClickedCommand = ReactiveCommand.Create<string>(markedCardValue =>
			{
				MessageBus.Current.SendMessage(new CardMarkedMessage(markedCardValue));
			});
		}

		// Clean up resources
		public void Dispose()
		{
			_disposables.Dispose();
		}
	}
}
