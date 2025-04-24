// Project: BlackJackV2
// file: BlackJackV2/ViewModels/ButtonViewModel.cs

///	<summary>
///		This class is responsible for handling the button commands in the game.
///		
///		PlayerRound					_playerRound		: The logic for the player round
///		
///		ReactiveCommand<Unit, Unit> HitCommand			: The command for the hit action 
///		ReactiveCommand<Unit, Unit> FoldCommand			: The command for the fold action
///		ReactiveCommand<Unit, Unit> DoubleDownCommand	: The command for the double down action
///		ReactiveCommand<Unit, Unit> SplitCommand		: The command for the split action
///
/// </summary>

using Avalonia.Media.Imaging;
using BlackJackV2.Constants;
using BlackJackV2.Models.GameLogic;
using ReactiveUI;
using System.Reactive;

namespace BlackJackV2.ViewModels
{
	public class ButtonViewModel : ReactiveObject
	{
		private IPlayerRound<Bitmap, string> _playerRound;
		
		public ReactiveCommand<Unit, Unit> HitCommand { get; }
		public ReactiveCommand<Unit, Unit> FoldCommand { get; }
		public ReactiveCommand<Unit, Unit> DoubleDownCommand { get; }
		public ReactiveCommand<Unit, Unit> SplitCommand { get; }
		
		public ButtonViewModel(IPlayerRound<Bitmap, string> playerRound)
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
		}
	}
}
