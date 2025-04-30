// Project: BlackJackV2
// file: BlackJackV2/DependencyInjection/ServiceRegistration.cs

using Avalonia.Media.Imaging;
using BlackJackV2.Factories.CardDeckFactory;
using BlackJackV2.Factories.CardFactory;
using BlackJackV2.Factories.CardHandFactory;
using BlackJackV2.Factories.PlayerFactory;
using BlackJackV2.Factories.PlayerHandsFactory;
using BlackJackV2.Models.GameLogic;
using BlackJackV2.Models.GameLogic.PlayerServices;
using BlackJackV2.Models.GameLogic.GameRuleServices;
using BlackJackV2.Models.Player;
using BlackJackV2.Services.Events;
using BlackJackV2.Shared.Constants;
using BlackJackV2.Shared.Utilities.ImageLoader;
using BlackJackV2.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Reactive.Subjects;
using BlackJackV2.Models.GameLogic.GameRuleServices.Interfaces;
using BlackJackV2.Models.GameLogic.Dealer_Services;
using BlackJackV2.Models.GameLogic.CoreServices;
using System.Collections.Generic;
using BlackJackV2.Models.GameLogic.CardServices;
using BlackJackV2.Factories.PlayerViewModelFactory;
using BlackJackV2.Factories.CardHandViewModelFactory;
using BlackJackV2.Factories.ButtonViewModelFactory;
using BlackJackV2.ViewModels.Interfaces;

namespace BlackJackV2.Services.DependencyInjection
{
	/// <summary>
	/// This class contains extension methods for the <see cref="IServiceCollection"/> interface.
	/// </summary>
	public static class ServiceCollectionExtensions
	{
		//TODO: SEPERATE into different methods.(views, models, and so on.. )
		/// <summary>
		/// This method is used to register the services for the game logic.
		/// </summary>
		/// <param name="collection">The service collection to add the services to.</param>
		public static void AddLogicServices(this IServiceCollection collection) 
		{
			collection.AddSingleton<IImageLoader<Bitmap>, AvaloniaImageLoader>();

			collection.AddScoped<Dictionary<string, IPlayer<Bitmap, string>>>();
			
			// Subjects 
			collection.AddSingleton<Subject<PlayerActionEvent>>();
			collection.AddSingleton<Subject<Dictionary<string, IPlayer<Bitmap, string>>>>();
			collection.AddSingleton<Subject<SplitSuccessfulEvent>>();
			collection.AddSingleton<Subject<BetUpdateEvent>>();

			// Models factories
			collection.AddSingleton<BlackJackCardCreator<Bitmap, string>>();
			collection.AddSingleton<BlackJackCardDeckCreator<Bitmap, string>>();
			collection.AddSingleton<BlackJackCardHandCreator<Bitmap, string>>();
			collection.AddSingleton<BlackJackPlayerHandsCreator<Bitmap, string>>();
			collection.AddSingleton<BlackJackPlayerCreator<Bitmap, string>>();

			// Rule Services
			collection.AddSingleton<IGameRules<Bitmap, string>, GameRules<Bitmap, string>>();
			collection.AddSingleton<IRoundEvaluator<Bitmap, string>, RoundEvaluator<Bitmap, string>>();
			
			// PlayerServices
			collection.AddSingleton<IPlayerRound<Bitmap, string>, PlayerRound<Bitmap, string>>();
			collection.AddSingleton<IPlayerAction<Bitmap, string>, PlayerAction<Bitmap, string>>();

			// Game logic services
			collection.AddSingleton<ICardServices<Bitmap, string>, CardServices<Bitmap, string>>();
			collection.AddSingleton<IDealerServices<Bitmap, string>, DealerServices<Bitmap, string>>();
			collection.AddSingleton<IPlayerServices<Bitmap, string>, PlayerServices<Bitmap, string>>();
			collection.AddSingleton<GameRuleServices<Bitmap, string>>();
			collection.AddSingleton<IGameCoordinator<Bitmap, string>, GameCoordinator<Bitmap, string>>();


			// Model 
			collection.AddSingleton<IPlayer<Bitmap, string>, Player<Bitmap, string>>();
			collection.AddSingleton<GameLogic<Bitmap, string>>();

			// View models factories
			collection.AddSingleton<BlackJackPlayerViewModelCreator>();
			collection.AddSingleton<BlackJackCardHandViewModelCreator>();
			collection.AddSingleton<BlackJackButtonViewModelCreator>();

			// View models
			collection.AddSingleton<InformationViewModel>();
			collection.AddScoped<IButtonViewModel, ButtonViewModel>();
			collection.AddSingleton<TableViewModel>();
			collection.AddSingleton<MainWindowViewModel>();
		}
	}
}
