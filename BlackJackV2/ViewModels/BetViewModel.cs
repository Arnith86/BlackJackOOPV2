// Project: BlackJackV2
// file: BlackJackV2/ViewModels/BetViewModel.cs

using Avalonia.Media.Imaging;
using BlackJackV2.Models.GameLogic.GameRuleServices;
using BlackJackV2.Models.GameLogic.GameRuleServices.Interfaces;
using BlackJackV2.Models.GameLogic.PlayerServices;
using BlackJackV2.Models.Player;
using BlackJackV2.ViewModels.Interfaces;
using ReactiveUI;
using System.Diagnostics;
using System.Reactive;
using System.Text.RegularExpressions;

namespace BlackJackV2.ViewModels
{
	/// <summary>
	/// ViewModel responsible for handling bet input logic in the Blackjack game.
	/// </summary>
	public class BetViewModel : ReactiveObject, IBetViewModel
	{
		private readonly Regex InputBetRegex = new Regex(@"^\d+$");
		private IPlayer<Bitmap, string> _player;
		private IGameRules<Bitmap, string> _gameRule;
		private IPlayerServices<Bitmap, string> _playerServices;
		private bool _canPlaceBet;

		/// <inheritdoc/>
		public ReactiveCommand<string, Unit> InputBetCommand { get; }


		/// <summary>
		/// Initializes a new instance of the <see cref="BetViewModel"/> class.
		/// Sets up the input command and connects to the necessary services.
		/// </summary>
		/// <remarks>
		/// Related files <see cref="BlackJackV2.Factories.BetViewModelFactory"/>
		/// </remarks>
		/// <param name="player">The player placing the bet.</param>
		/// <param name="playerServices">Service for sending the bet to the hand.</param>
		/// <param name="gameRule">Service for validating the bet amount based on game rules.</param>
		public BetViewModel(
			IPlayer<Bitmap, string> player,
			IPlayerServices<Bitmap, string> playerServices,
			IGameRules<Bitmap, string> gameRule)
		{
			_player = player;
			_gameRule = gameRule;
			_playerServices = playerServices;
			CanPlaceBet = true;

			// ReactiveCommand to handle bet input, as long as CanPlaceBet is true.
			var canExecute = this.WhenAnyValue(x => x.CanPlaceBet);
			InputBetCommand = ReactiveCommand.Create<string>(OnBetEntered, canExecute); 
		}

		/// <inheritdoc/>
		public bool CanPlaceBet
		{
			get => _canPlaceBet;
			set => this.RaiseAndSetIfChanged(ref _canPlaceBet, value);
		}

		private void OnBetEntered(string betString)
		{
			// Validates the bet input. Must be a number between 1 and 10, and less than or equal to Funds 
			if (!string.IsNullOrWhiteSpace(betString) &&
					InputBetRegex.IsMatch(betString) &&
					int.TryParse(betString, out int parsedBet))
			{
				var result = _gameRule.CanPlaceInitialBet(_player, parsedBet);

				if (!result.IsAllowed)
				{
					//TODO: Show the user that the bet is not allowed
					Debug.WriteLine(result.Message);
				}
				else
				{
					_playerServices.OnBetInputReceived(_player.Name, parsedBet);
					CanPlaceBet = false;
				}
			}
			else
			{
				// TODO: IMPLEMENT proper error handling
				Debug.WriteLine("Invalid input: not a number.");
			}
		}

		/// <inheritdoc/>
		public void Dispose() => 
			InputBetCommand.Dispose();
	}
}
