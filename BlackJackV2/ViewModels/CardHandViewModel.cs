// Project: BlackJackV2
// file: BlackJackV2/ViewModels/CardHandViewModel.cs

using System;
using System.Collections.ObjectModel;
using System.Reactive.Disposables;
using Avalonia.Media.Imaging;
using BlackJackV2.Models.Card;
using BlackJackV2.Models.CardHand;
using BlackJackV2.Shared.Constants;
using BlackJackV2.ViewModels.Interfaces;
using ReactiveUI;

namespace BlackJackV2.ViewModels
{
	public class CardHandViewModel : ReactiveObject, ICardHandViewModel
	{
		private HandOwners.HandOwner _primaryOrSplit;
		private int _bet;
		private bool _handIsActive;
		private string _handValue;
		private ObservableCollection<ICard<Bitmap, string>> _cards;
		private readonly CompositeDisposable _disposables = new CompositeDisposable();
		
		/// <summary>
		/// ViewModel for action buttons associated with the hand (e.g., Hit, Stand).
		/// </summary>
		public IButtonViewModel ButtonViewModel { get; }

		/// <summary>
		/// ViewModel for regestering bets associated with the hand.
		/// </summary>
		public IBetViewModel BetViewModel { get; }

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
		public CardHandViewModel(IBlackJackCardHand<Bitmap, string> cardHand, IInputWrapperViewModel inputWrapperViewModel)
		{
			Initialize(cardHand.Id, cardHand);
			
			HandIsActive = cardHand.IsActive;
			Bet = 0;
			
			ButtonViewModel = inputWrapperViewModel.ButtonViewModel;
			BetViewModel = inputWrapperViewModel.BetViewModel;

			// Automatically update UI-bound properties when model changes
			cardHand.WhenAnyValue(x => x.IsActive).Subscribe( isActive => {
				HandIsActive = isActive;
				ButtonViewModel.HandIsActive = isActive;
			}).DisposeWith(_disposables);
		}

		/// <summary>
		/// Dealer specific <see cref="CardHandViewModel"/>."/>
		/// Initializes a new instance of the <see cref="CardHandViewModel"/> class,
		/// binding state to a Blackjack card hand.
		/// </summary>
		/// <param name="cardHand">The underlying hand model.</param>
		public CardHandViewModel(IBlackJackCardHand<Bitmap, string> cardHand)
		{
			Initialize(cardHand.Id, cardHand);
		}

		/// <summary>
		/// Initializes shared properties for both dealer and player hands.
		/// </summary>
		/// <param name="primaryOrSplit">Indicates if the hand is primary or split.</param>
		/// <param name="cardHand">The underlying hand model.</param>
		private void Initialize(
			HandOwners.HandOwner primaryOrSplit, 
			IBlackJackCardHand<Bitmap, string> cardHand) 
		{
			_primaryOrSplit = cardHand.Id;
			_cards = cardHand.Hand;
			_handValue = FormatHandValue(cardHand.HandValue);

			// Automatically update UI-bound properties when model changes
			cardHand.WhenAnyValue(x => x.HandValue)
				.Subscribe(handValue => HandValue = FormatHandValue(handValue))
				.DisposeWith(_disposables);
		}

		// Changes format from int to string.
		private string FormatHandValue(int value) => value.ToString(); 

		/// <inheritdoc/>
		public HandOwners.HandOwner Id => _primaryOrSplit;

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
		public ObservableCollection<ICard<Bitmap, string>> Cards => _cards;
		
		/// <summary>
		/// Disposes of reactive subscriptions to prevent memory leaks.
		/// </summary>
		public void Dispose()
		{
			_disposables.Dispose();
		}
	}
}
