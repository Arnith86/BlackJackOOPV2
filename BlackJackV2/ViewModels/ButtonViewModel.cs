using BlackJackV2.Constants;
using BlackJackV2.Models.GameLogic;
using BlackJackV2.Services.Messaging;
using BlackJackV2.Constants;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackV2.ViewModels
{

	/**
	* This class is responsible for handling the button commands in the game.
	**/

	public class ButtonViewModel : ReactiveObject
	{
		private PlayerRound _playerRound;
		private HandOwners.HandOwner _activeHand;

		public ReactiveCommand<Unit, Unit> HitCommand { get; }
		public ReactiveCommand<Unit, Unit> FoldCommand { get; }
		public ReactiveCommand<Unit, Unit> DoubleDownCommand { get; }
		public ReactiveCommand<Unit, Unit> SplitCommand { get; }
		private string markedCardValue = string.Empty; // remove when finished

		public ButtonViewModel(PlayerRound playerRound)
		{
			_playerRound = playerRound;

			HitCommand = ReactiveCommand.Create(() => 
				_playerRound._playerActionSubject.OnNext(BlackJackActions.PlayerActions.Hit)); 

			FoldCommand = ReactiveCommand.Create(() => 
				_playerRound._playerActionSubject.OnNext(BlackJackActions.PlayerActions.Fold));
			
			DoubleDownCommand = ReactiveCommand.Create(() =>
				_playerRound._playerActionSubject.OnNext(BlackJackActions.PlayerActions.DoubleDown));

			SplitCommand = ReactiveCommand.Create(() =>
				_playerRound._playerActionSubject.OnNext(BlackJackActions.PlayerActions.Split));
			
			// Subscribe to the CardMarkedMessage
			MessageBus.Current.Listen<CardMarkedMessage>()
				.Subscribe(message =>  markedCardValue = message.MarkedCardValue);

			// Subscribe to the ActiveHandMessage
			MessageBus.Current.Listen<ActiveHandMessage>()
				.Subscribe(message =>
				{
					_activeHand = message.ActiveHand;
				});
		}
	}
}
