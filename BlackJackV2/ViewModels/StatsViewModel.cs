using BlackJackV2.Models.GameLogic;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BlackJackV2.ViewModels
{

	/**
	 * This view model is used to show current points, and register the bet input from the user.
	 **/

	public class StatsViewModel : ReactiveObject
	{
		private readonly Regex InputBetRegex = new Regex(@"^\d+$");

		private int _points;
		private bool _isBetEnabled;

		public int Points
		{
			get => _points;
			set => this.RaiseAndSetIfChanged(ref _points, value);
		}

		public bool IsBetEnabled
		{
			get => _isBetEnabled;
			set => this.RaiseAndSetIfChanged(ref _isBetEnabled, value);
		}

		public ReactiveCommand<string, Unit> InputBetCommand { get; }

		public StatsViewModel(GameLogic gameLogic)
		{
			Points = 10;
			IsBetEnabled = true;

			InputBetCommand = ReactiveCommand.Create<string>(betString =>
			{
				// Ignore Enter key press if the bet is not enabled
				if (!IsBetEnabled) return;

				// Validates the bet input. Must be a number between 1 and 10, and less than or equal to Points 
				if ( !string.IsNullOrWhiteSpace(betString) && InputBetRegex.IsMatch(betString) &&
					int.TryParse(betString, out int parsedBet) && 
					(parsedBet < 11 && parsedBet > 0) && parsedBet <= Points)
				{
					gameLogic.OnBetInputReceived(parsedBet);
				}
				else
				{

					// TODO: IMPLEMENT proper error handling
					Debug.WriteLine("Invalid input: not a number.");
				}
			});


			// This will automatically update bet in the UI due to data binding
			gameLogic.GameStateObservable.Subscribe(gameState =>
			{
				Points = gameState.Points;
				IsBetEnabled = !gameState.IsBetRecieved;
			});

		}
	}
}
