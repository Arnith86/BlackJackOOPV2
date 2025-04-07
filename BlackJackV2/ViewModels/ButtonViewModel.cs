using BlackJackV2.Models.GameLogic;
using BlackJackV2.Services.Messaging;
using ReactiveUI;
using System;
using System.Collections.Generic;
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
		private GameLogic _gameLogic;
		public ReactiveCommand<Unit, Unit> HitCommand { get; }
		public ReactiveCommand<Unit, Unit> FoldCommand { get; }
		public ReactiveCommand<Unit, Unit> DoubleDownCommand { get; }
		public ReactiveCommand<Unit, Unit> SplitCommand { get; }

		public ButtonViewModel(GameLogic gameLogic)
		{
			_gameLogic = gameLogic;

			HitCommand = ReactiveCommand.Create(() => gameLogic.HitAction());
			FoldCommand = ReactiveCommand.Create(() => Console.WriteLine("Fold pressed"));
			DoubleDownCommand = ReactiveCommand.Create(() => Console.WriteLine("Double pressed"));
			SplitCommand = ReactiveCommand.Create(() =>
			{
				SplitPerformed(gameLogic.SplitAction()); 
			});
			
			// Subscribe to the CardMarkedMessage
			MessageBus.Current.Listen<CardMarkedMessage>()
				.Subscribe(message =>  markedCardValue = message.MarkedCardValue);
		}

		// This method is called when the player attempts to splits their hand
		// The result of the split is sent as a message to the rest of the application
		private void SplitPerformed(bool yesNo) 
		{
			MessageBus.Current.SendMessage(new SplitSuccessfulMessage(yesNo));
		}
	}
}
