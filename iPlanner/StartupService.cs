﻿using iPlanner.Core.Application.DTO;
using iPlanner.Core.Application.DTO.Reports;
using iPlanner.Core.Application.DTO.Teams;
using iPlanner.Core.Application.Interfaces;
using iPlanner.Core.Application.Interfaces.Repository;
using iPlanner.Core.Application.Mappers;
using iPlanner.Core.Application.Services;
using iPlanner.Core.Entities.Locations;
using iPlanner.Core.Entities.Reports;
using iPlanner.Core.Entities.Teams;
using iPlanner.Infrastructure.Common;
using iPlanner.Infrastructure.Locations;
using iPlanner.Infrastructure.Reports;
using iPlanner.Infrastructure.Teams;
using iPlanner.Presentation.Commands;
using iPlanner.Presentation.Commands.Reports;
using iPlanner.Presentation.Commands.Teams;
using iPlanner.Presentation.Commands.Window;
using iPlanner.Presentation.Controls.Layout.RibbonTools;
using iPlanner.Presentation.Interfaces;
using iPlanner.Presentation.Services;
using iPlanner.Presentation.ViewModels.Layout;
using iPlanner.Presentation.ViewModels.Reports;
using iPlanner.Presentation.ViewModels.Teams;
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
            services.AddSingleton<AddTeamCommand>();
            services.AddSingleton<RemoveTeamsCommand>();
            services.AddSingleton<CloseFormCommand>();
            services.AddSingleton<RemoveLocationReportCommand>();
            services.AddSingleton<AddLocationReportCommand>();
            services.AddSingleton<CreateReportCommand>();            
            services.AddSingleton<SyncReportFilterCommand>();

            //Factories and Mediator
            services.AddSingleton<IControlAbstractFactory, ControlFactory>();
            services.AddSingleton<IMediator, AppMediatorService>();

            //Repositories
            services.AddSingleton<IReportRepository, FileReportsRepository>();
            services.AddSingleton<ITeamsRepository, FileTeamsRepository>();
            services.AddSingleton<ILocationsRepository, ExternalLibraryLocationsRepository>();

            //Mappers
            services.AddSingleton<IMapper<ReportDTO, Report>, ReportMapper>();
            services.AddSingleton<IMapper<ReportFilterDTO, ReportFilter>, ReportFilterMapper>();
            services.AddSingleton<IMapper<ActivityDTO, Activity>, ActivityMapper>();
            services.AddSingleton<IMapper<LocationItemDTO, LocationItem>, LocationItemMapper>();
            services.AddSingleton<IMapper<TeamDTO, Team>, TeamMapper>();
            services.AddSingleton<IMapper<TeamMemberDTO, TeamMember>, TeamMemberMapper>();
            services.AddSingleton<IMapper<ScheduleTeamDataDTO, ScheduleTeamData>, ScheduleTeamDataMapper>();


            //Services
            services.AddSingleton<ILocationService, LocationService>();
            services.AddSingleton<IReportService, ReportService>();
            services.AddSingleton<ITeamService, TeamsService>();
            services.AddSingleton<ITeamScheduleService, TeamScheduleService>();
            services.AddSingleton<FileService>();
            services.AddSingleton<IConflictValidationService, ConflictValidationService>();

            //ViewModells
            services.AddSingleton<RibbonViewModel>();
            services.AddSingleton<ReportsFilterViewmodel>();
            services.AddTransient<CalendarService>();
            services.AddTransient<DashboardService>();
            services.AddTransient<TeamFormViewModel>();
            services.AddTransient<TeamsViewModel>();
            services.AddTransient<ReportListViewModel>();

            //View Helpers
            services.AddSingleton<DockingManagerEventsHandler>();
            services.AddSingleton<DockingManagerUpdater>();
            services.AddSingleton<SidePanelManager>();

            //View
            services.AddSingleton<MainWindow>();
        }
    }
}
