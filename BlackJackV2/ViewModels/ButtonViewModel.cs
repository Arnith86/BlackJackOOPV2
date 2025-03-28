using BlackJackV2.Models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackV2.ViewModels
{
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
			//HitCommand = ReactiveCommand.Create(() => { });
			//FoldCommand = ReactiveCommand.Create(() => { });
			//DoubleDownCommand = ReactiveCommand.Create(() => { });
			//SplitCommand = ReactiveCommand.Create(() => { });
			HitCommand = ReactiveCommand.Create(() => gameLogic.AddCard());
			FoldCommand = ReactiveCommand.Create(() => Console.WriteLine("Fold pressed"));
			DoubleDownCommand = ReactiveCommand.Create(() => Console.WriteLine("Double pressed"));
			SplitCommand = ReactiveCommand.Create(() => Console.WriteLine("Split pressed"));
		}
	}
}
