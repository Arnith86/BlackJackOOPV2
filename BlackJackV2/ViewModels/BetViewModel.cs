using Avalonia.Media.Imaging;
using BlackJackV2.Models.GameLogic.GameRuleServices.Interfaces;
using BlackJackV2.Models.GameLogic.PlayerServices;
using BlackJackV2.Models.Player;
using BlackJackV2.Services.Events;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive;
using System.Reactive.Subjects;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BlackJackV2.ViewModels
{
	public class BetViewModel : ReactiveObject
	{
		private readonly Regex InputBetRegex = new Regex(@"^\d+$");
		private readonly IPlayer<Bitmap, string> _player;
		private bool _canPlaceBet;

		public ReactiveCommand<string, Unit> InputBetCommand { get; }

		////////////////////////////////////////////////////////////////////////////// CONTINUE FROM HERE !!!////////////////////////////

		/// <param name="betRequestEvent">Event triggered when a bet is requested from the player.</param>
		public BetViewModel(
			IPlayer<Bitmap, string> player,
			IPlayerServices<Bitmap, string> playerServices,
			IGameRules<Bitmap, string> gameRule,
			Subject<BetRequestEvent<Bitmap, string>> betRequestEvent)
		{
			CanPlaceBet = true;

			InputBetCommand = ReactiveCommand.Create<string>(betString =>
			{
				// Ignore Enter key press if the bet is not enabled
				if (!CanPlaceBet) return;

				// Validates the bet input. Must be a number between 1 and 10, and less than or equal to Funds 
				if (!string.IsNullOrWhiteSpace(betString) &&
						InputBetRegex.IsMatch(betString) &&
						int.TryParse(betString, out int parsedBet))
				{
					var result = gameRule.CanPlaceInitialBet(player, parsedBet);
					if (!result.IsAllowed)
					{
						//TODO: Show the user that the bet is not allowed
						Debug.WriteLine(result.Message);
					}
					else
					{
						playerServices.OnBetInputReceived(player.Name, parsedBet);
						CanPlaceBet = false; 
					}
				}
				else
				{
					// TODO: IMPLEMENT proper error handling
					Debug.WriteLine("Invalid input: not a number.");
				}
			}); 
		}

		public bool CanPlaceBet
		{
			get => _canPlaceBet;
			set => this.RaiseAndSetIfChanged(ref _canPlaceBet, value);
		}
	}
}
