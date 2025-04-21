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
using BlackJackV2.Models.CardDeck;
using BlackJackV2.Models.GameLogic;
using BlackJackV2.Models.PlayerHands;
using BlackJackV2.Services.Events;
using BlackJackV2.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace BlackJackV2.Services.DependencyInjection
{
	public static class ServiceCollectionExtensions
	{
		public static void AddLogicServices(this IServiceCollection collection) 
		{
			
			collection.AddSingleton<ICardCreator<Bitmap, string>, BlackJackCardCreator>();
			collection.AddSingleton<ICardDeckCreator<Bitmap, string>, BlackJackCardDeckCreator>();

			collection.AddSingleton<ICardHandCreator<Bitmap, string>, BlackJackCardHandCreator>();

			//collection.AddSingleton<IBlackJackPlayerHands<Bitmap, string>, BlackJackPlayerHands>();
			collection.AddSingleton<IBlackJackPlayerHandsCreator<Bitmap, string>, BlackJackPlayerHandsCreator>();
	
			collection.AddSingleton<IPlayerCreator<Bitmap, string>, PlayerCreator>();

			collection.AddSingleton<BetUpdateEvent>();

			collection.AddSingleton<GameLogic>();

			collection.AddSingleton<MainWindowViewModel>();
		}
	}
}
