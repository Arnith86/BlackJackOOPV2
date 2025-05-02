// Project: BlackJackV2
// file: BlackJackV2/ViewModels/CardHandViewModel.cs

using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Disposables;
using Avalonia.Media.Imaging;
using BlackJackV2.Models.Card;
using BlackJackV2.Models.CardHand;
using BlackJackV2.Services.Messaging;
using BlackJackV2.Shared.Constants;
using BlackJackV2.ViewModels.Interfaces;
using ReactiveUI;

namespace BlackJackV2.ViewModels
{
	public class CardHandViewModel : ReactiveObject, ICardHandViewModel
	{
		private HandOwners.HandOwner _id;
		private int _bet;
		private bool _handIsActive;
		private string _handValue;
		private ObservableCollection<ICard<Bitmap, string>> _cards;
		private readonly CompositeDisposable _disposables = new CompositeDisposable();

		/// <summary>
		/// Command triggered when a card is clicked, sending a CardMarkedMessage.
		/// </summary>
		public ReactiveCommand<string, Unit> CardClickedCommand { get; }
		
		/// <summary>
		/// ViewModel for action buttons associated with the hand (e.g., Hit, Stand).
		/// </summary>
		public IButtonViewModel ButtonViewModel { get; }

		/// <summary>
		/// ViewModel for regestering bets associated with the hand.
		/// </summary>
		public BetViewModel BetViewModel { get; }

		/// <summary>
		/// Player specific <see cref="CardHandViewModel"/>.
		/// Initializes a new instance of the <see cref="CardHandViewModel"/> class,
		/// binding state to a Blackjack card hand and subscribing to updates.
		/// </summary>
		/// /// <remarks>
		/// Related files <see cref="BlackJackV2.Factories.CardHandViewModelFactory"/>
		/// </remarks>
		/// <param name="cardHand">The underlying hand model.</param>
		/// <param name="inputWrapperViewModel">ViewModel for input actions.</param>
		public CardHandViewModel(IBlackJackCardHand<Bitmap, string> cardHand, InputWrapperViewModel inputWrapperViewModel)
		{
			_id = cardHand.Id;
			HandIsActive = cardHand.IsActive;
			Bet = 0;
			_cards = cardHand.Hand;
			_handValue = cardHand.HandValue.ToString();

			ButtonViewModel = inputWrapperViewModel.ButtonViewModel;
			
			//TODO: Handle bet and bet request events!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
			BetViewModel = inputWrapperViewModel.BetViewModel;

			// Automatically update UI-bound properties when model changes
			cardHand.WhenAnyValue(x => x.IsActive, x => x.HandValue)
				.Subscribe(tuple =>
				{
					var (isActive, handValue) = tuple;
					
					HandIsActive = isActive;
					ButtonViewModel.HandIsActive = isActive;
					
					HandValue = handValue.ToString();
				})
				.DisposeWith(_disposables);


			// Define the command to mark a card when clicked
			CardClickedCommand = ReactiveCommand.Create<string>(markedCardValue =>
			{
				MessageBus.Current.SendMessage(new CardMarkedMessage(markedCardValue));
			});
		}

		/// <summary>
		/// Dealer specific <see cref="CardHandViewModel"/>."/>
		/// Initializes a new instance of the <see cref="CardHandViewModel"/> class,
		/// binding state to a Blackjack card hand.
		/// </summary>
		/// <param name="cardHand">The underlying hand model.</param>
		public CardHandViewModel(IBlackJackCardHand<Bitmap, string> cardHand)
		{
			_id = cardHand.Id;
			_cards = cardHand.Hand;
			_handValue = cardHand.HandValue.ToString();

			// Automatically update UI-bound properties when model changes
			cardHand.WhenAnyValue(x => x.HandValue)
				.Subscribe(handValue =>
				{
					//var (handValue) = tuple;
					HandValue = handValue.ToString();
				})
				.DisposeWith(_disposables);
		}

		/// <inheritdoc/>
		public HandOwners.HandOwner Id => _id;

		/// <inheritdoc/>
		public int Bet
		{
			get => _bet;
			set => this.RaiseAndSetIfChanged(ref _bet, value);
		}

		/// <inheritdoc/>
		public bool HandIsActive
		{
			get => _handIsActive;
			set => this.RaiseAndSetIfChanged(ref _handIsActive, value);
		}

		/// <inheritdoc/>
		public string HandValue
		{
			get => _handValue;
			set => this.RaiseAndSetIfChanged(ref _handValue, value);
		}

		/// <inheritdoc/>
		public ObservableCollection<ICard<Bitmap, string>> Cards
		{
			get => _cards;
			set => this.RaiseAndSetIfChanged(ref _cards, value);
		}

		/// <summary>
		/// Disposes of reactive subscriptions to prevent memory leaks.
		/// </summary>
		public void Dispose()
		{
			_disposables.Dispose();
		}
	}
}
