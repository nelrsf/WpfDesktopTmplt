using iPlanner.Core.Application.Interfaces;
using iPlanner.Core.Application.Services;
using iPlanner.Infrastructure.Locations;
using iPlanner.Infrastructure.Reports;
using iPlanner.Infrastructure.Teams;
using iPlanner.Presentation.Commands;
using iPlanner.Presentation.Commands.Teams;
using iPlanner.Presentation.Commands.Window;
using iPlanner.Presentation.Services;
using iPlanner.Presentation.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace iPlanner
{
    public static class StartupService
    {
        public static void ConfigureServices(ServiceCollection services)
        {
            //Commands
            services.AddSingleton<ArrangeVerticalCommand>();
            services.AddSingleton<ArrangeHorizontalCommand>();
            services.AddSingleton<ArrangeCascadeCommand>();
            services.AddSingleton<ArrangeGridCommand>();
            services.AddSingleton<OpenNewDialog>();
            services.AddSingleton<InsertNewViewCommand>();
            services.AddSingleton<SelectTabCommand>();
            services.AddSingleton<ToggleSideBarCommand>();
            services.AddSingleton<AddMemberCommand>();
            services.AddSingleton<SaveTeamsCommand>();
            services.AddSingleton<RemoveTeamsCommand>();
            services.AddSingleton<CloseTeamsFormCommand>();

            services.AddSingleton<ICommandFactory, CommandFactory>();
            services.AddSingleton<IMediator, AppMediatorService>();

            //Services
            services.AddSingleton<ILocationService, LocationService>();
            services.AddSingleton<IReportService, ReportService>();
            services.AddSingleton<ITeamService, TeamsService>();

            //ViewModells
            services.AddSingleton<RibbonViewModel>();
            services.AddSingleton<CalendarService>();
            services.AddSingleton<DashboardService>();
            services.AddSingleton<TeamFormViewModel>();
            services.AddSingleton<TeamsViewModel>();

            //View
            services.AddSingleton<IMainWindow, MainWindow>();
        }
    }
}
