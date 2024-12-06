using iPlanner.Core.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;


namespace iPlanner
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IServiceProvider? serviceProvider;
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ServiceCollection services = new ServiceCollection();
            StartupService.ConfigureServices(services);
            AppServices.ServiceProvider = services.BuildServiceProvider();
            IMainWindow mainWindow = AppServices.GetService<IMainWindow>();
            mainWindow.Show();
        }
    }

}
