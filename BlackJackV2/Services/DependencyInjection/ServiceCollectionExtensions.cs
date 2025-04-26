// Project: BlackJackV2
// file: BlackJackV2/DependencyInjection/ServiceRegistration.cs

/// <summary>
///		This class manages the creation and lifetime of objects, and makes sure each class gets its required dependencies (without you manually wiring them up).
/// </summary>

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

namespace BlackJackV2.Services.DependencyInjection
{
	public static class ServiceCollectionExtensions
	{
		//TODO: SEPERATE into different methods.(views, models, and so on.. )
		public static void AddLogicServices(this IServiceCollection collection) 
		{
			collection.AddSingleton<IImageLoader<Bitmap>, AvaloniaImageLoader>();

			collection.AddSingleton<Subject<BlackJackActions.PlayerActions>>();
			collection.AddSingleton<Subject<SplitSuccessfulEvent>>();
			collection.AddSingleton<Subject<BetUpdateEvent>>();

			collection.AddSingleton<IGameRuleServices<Bitmap, string>, GameRuleService<Bitmap, string>>();

			collection.AddSingleton<CardCreator<Bitmap, string>, BlackJackCardCreator<Bitmap, string>>();
			collection.AddSingleton<CardDeckCreator<Bitmap, string>, BlackJackCardDeckCreator<Bitmap, string>>();

			collection.AddSingleton<CardHandCreator<Bitmap, string>, BlackJackCardHandCreator<Bitmap, string>>();
			collection.AddSingleton<PlayerHandsCreator<Bitmap, string>, BlackJackPlayerHandsCreator<Bitmap, string>>();
	
			collection.AddSingleton<PlayerCreator<Bitmap, string>, BlackJackPlayerCreator<Bitmap, string>>();
			


			collection.AddSingleton<IGameCoordinator<Bitmap, string>, GameCoordinator<Bitmap, string>>();


			collection.AddSingleton<IPlayer<Bitmap, string>, Player<Bitmap, string>>();

			collection.AddSingleton<IPlayerRound<Bitmap, string>, PlayerRound<Bitmap, string>>();
			collection.AddSingleton<PlayerAction<Bitmap, string>>();
			collection.AddSingleton<GameLogic<Bitmap, string>>();
			
		

			collection.AddSingleton<TableViewModel>();
			collection.AddSingleton<MainWindowViewModel>();
		}
	}
}
