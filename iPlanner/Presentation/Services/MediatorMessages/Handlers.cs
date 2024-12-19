using iPlanner.Core.Application.Interfaces;
using iPlanner.Presentation.Commands;

namespace iPlanner.Presentation.Services.MediatorMessages
{

    public class CommandMessageHandler : IMessageHandler<CommandMessage>
    {
        private readonly ICommandFactory _commandFactory;
        private readonly IMainWindow _window;

        public CommandMessageHandler(ICommandFactory commandFactory, IMainWindow window)
        {
            _commandFactory = commandFactory;
            _window = window;
        }

        public void Handle(CommandMessage message)
        {
            var command = _commandFactory.GetCommand(message.CommandType);
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
        private readonly ICommand? _addTeamCommand;
        private readonly ICommand? _addMemberCommand;
        private readonly IWindowCommand? _closeFormCommand;
        private readonly ICommand? _removeTeamsCommand;
        private readonly IMainWindow _window;
        private readonly ICommandFactory _commandFactory;

        public TeamMessageHandler(ICommandFactory commandFactory, IMainWindow window)
        {
            _commandFactory = commandFactory;
            _addTeamCommand = _commandFactory.GetCommand(CommandType.AddTeam);
            _addMemberCommand = _commandFactory.GetCommand(CommandType.AddTeamMember);
            _removeTeamsCommand = _commandFactory.GetCommand(CommandType.DeleteTeams);
            _closeFormCommand = (IWindowCommand)_commandFactory.GetCommand(CommandType.CloseForm);
            _window = window;
            _closeFormCommand.MainWindow = _window;
        }


        public void Handle(TeamMessage message)
        {
            switch (message.CommandType)
            {
                case CommandType.AddTeam:
                    _addTeamCommand?.Execute(message.TeamToCreate);
                    _closeFormCommand?.Execute(message.sender);
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
        private ICommandFactory _commandFactory;
        private ICommand? AddLocationsToReport;
        private ICommand? RemoveLocationsFromReport;
        private ICommand? CreateReportCommand;
        private IWindowCommand? CloseFormCommand;


        public ReportMessageHandler(ICommandFactory commandFactory)
        {
            _commandFactory = commandFactory;
            AddLocationsToReport = _commandFactory.GetCommand(CommandType.AddLocationsToReport);
            RemoveLocationsFromReport = _commandFactory.GetCommand(CommandType.RemoveLocationsFromReport);
            CreateReportCommand = _commandFactory?.GetCommand(CommandType.CreateReport);
            CloseFormCommand = (IWindowCommand?)_commandFactory.GetCommand(CommandType.CloseForm);
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
                    CloseFormCommand?.Execute(message.sender);
                    break;

            }

        }
    }


}
