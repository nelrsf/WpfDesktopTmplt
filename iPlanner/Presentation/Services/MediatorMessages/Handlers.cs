using iPlanner.Core.Application.DTO;
using iPlanner.Core.Application.Interfaces;
using iPlanner.Presentation.Commands;
using iPlanner.Presentation.Commands.Reports;
using iPlanner.Presentation.Commands.Teams;
using iPlanner.Presentation.Interfaces;
using System.Collections.ObjectModel;

namespace iPlanner.Presentation.Services.MediatorMessages
{

    public class CommandMessageHandler : IMessageHandler<CommandMessage>
    {
        private readonly IMainWindow _window;

        public CommandMessageHandler(IMainWindow window)
        {
            _window = window;
        }

        public void Handle(CommandMessage message)
        {
            ICommand<object> command = (ICommand<object>)Activator.CreateInstance(message.CommandType);
            command?.Execute(_window);
        }
    }

    public class ViewMessageHandler : IMessageHandler<ViewMessage>
    {
        private readonly InsertNewViewCommand _command;
        private readonly IMainWindow _window;


        public ViewMessageHandler(InsertNewViewCommand command, IMainWindow window)
        {
            _command = command;
            _window = window;
        }

        public void Handle(ViewMessage message)
        {
            _command.ViewName = message.ViewName;
            _command.Content = message.Content;
            _command.Execute(_window);
        }
    }


    public class TabMessageHandler : IMessageHandler<TabMessage>
    {
        private readonly SelectTabCommand _command;
        private readonly IMainWindow _window;

        public TabMessageHandler(SelectTabCommand command, IMainWindow window)
        {
            _command = command;
            _window = window;
        }

        public void Handle(TabMessage message)
        {
            _command.Document = message.Document;
            _command.Execute(_window);
        }
    }

    public class TeamMessageHandler : IMessageHandler<TeamMessage>
    {
        private readonly ICommand<TeamDTO> _addTeamCommand;
        private readonly ICommand<TeamDTO>? _addMemberCommand;
        private readonly IWindowCommand<IFormViewModel>? _closeFormCommand;
        private readonly ICommand<ObservableCollection<TeamDTO>>? _removeTeamsCommand;
        private readonly IMainWindow _window;
        private readonly ITeamService _teamService;
        //private readonly ICommandFactory _commandFactory;

        public TeamMessageHandler(IMainWindow window)
        {
            //_commandFactory = commandFactory;
            _teamService = AppServices.GetService<ITeamService>();
            _addTeamCommand = new AddTeamCommand(_teamService);
            _addMemberCommand = new AddMemberCommand();
            _removeTeamsCommand = new RemoveTeamsCommand(_teamService);
            _closeFormCommand = new CloseFormCommand();
            _window = window;
            _closeFormCommand.MainWindow = _window;
        }


        public void Handle(TeamMessage message)
        {
            switch (message.CommandType)
            {
                case CommandType.AddTeam:
                    _addTeamCommand?.Execute(message.TeamToCreate);
                    _closeFormCommand?.Execute((IFormViewModel?)message.sender);
                    break;
                case CommandType.AddTeamMember:
                    _addMemberCommand?.Execute(message.TeamToCreate);
                    break;
                case CommandType.DeleteTeams:
                    _removeTeamsCommand?.Execute(message.TeamsToRemove);
                    break;
            }
        }
    }


    public class ReportMessageHandler : IMessageHandler<ReportMessage>
    {
        //private ICommandFactory _commandFactory;
        private ICommand<ReportMessage>? AddLocationsToReport;
        private ICommand<ReportMessage>? RemoveLocationsFromReport;
        private ICommand<ReportMessage>? CreateReportCommand;
        private IWindowCommand<IFormViewModel>? CloseFormCommand;
        private IMainWindow _window;
        private IReportService _reportService;


        public ReportMessageHandler(IMainWindow mainWindow)
        {
            _reportService = AppServices.GetService<IReportService>();
            AddLocationsToReport = new AddLocationReportCommand();
            RemoveLocationsFromReport = new RemoveLocationReportCommand();
            CreateReportCommand = new CreateReportCommand(_reportService);
            CloseFormCommand = new CloseFormCommand();
            _window = mainWindow;
            CloseFormCommand.MainWindow = mainWindow;
        }
        public void Handle(ReportMessage message)
        {
            switch (message.CommandType)
            {
                case CommandType.AddLocationsToReport:
                    AddLocationsToReport?.Execute(message);
                    break;
                case CommandType.RemoveLocationsFromReport:
                    RemoveLocationsFromReport?.Execute(message);
                    break;
                case CommandType.CreateReport:
                    CreateReportCommand?.Execute(message);
                    CloseFormCommand?.Execute((IFormViewModel?)message.sender);
                    break;

            }

        }
    }


}
