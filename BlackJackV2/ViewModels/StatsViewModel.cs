using BlackJackV2.Models.GameLogic;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackV2.ViewModels
{
	public class StatsViewModel : ReactiveObject
	{

		private int _funds;
		private int _bet;

		public int Funds
		{
			get => _funds;
			set => this.RaiseAndSetIfChanged(ref _funds, value);
		}

		public int Bet
		{
			get => _bet;
			set => this.RaiseAndSetIfChanged(ref _bet, value);
		}

		public StatsViewModel(GameLogic gameLogic)
		{
			

			gameLogic.FundsAndBetObservable.Subscribe(fundsAndBet =>
				{
					// Update the funds and bet values when they change
					// This will automatically update the UI due to data binding
					Funds = fundsAndBet.Funds;
					Bet = fundsAndBet.Bet;
				});

			Funds = 0;
			Bet = 0;
		}
	}
}
