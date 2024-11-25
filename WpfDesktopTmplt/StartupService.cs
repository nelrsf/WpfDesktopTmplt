using Microsoft.Extensions.DependencyInjection;
using WpfDesktopTmplt.Core.Application.Interfaces;
using WpfDesktopTmplt.Core.Application.Services;
using WpfDesktopTmplt.Presentation.ViewModels;

namespace WpfDesktopTmplt
{
    public static class StartupService
    {
        public static void ConfigureServices(ServiceCollection services)
        {
            services.AddSingleton<IMainWindow, MainWindow>();
            services.AddSingleton<RibbonViewModel>();
            services.AddSingleton<CalendarService>();
            services.AddSingleton<DashboardService>();
        }
    }
}
