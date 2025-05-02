using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using BlackJackV2.Services.DependencyInjection;
using BlackJackV2.ViewModels;
using BlackJackV2.Views;
using Microsoft.Extensions.DependencyInjection;

namespace BlackJackV2
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

		public override void OnFrameworkInitializationCompleted()
        {
            // Register all the services needed for the application to run
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddLogicServices();

            // Creates a ServiceProvider containing services from the provided IServiceCollection
            var services = serviceCollection.BuildServiceProvider();
            var vm = services.GetRequiredService<MainWindowViewModel>();

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow
                {
                    DataContext = vm,
				};
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}