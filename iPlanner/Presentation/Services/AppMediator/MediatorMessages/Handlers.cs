using iPlanner.Core.Application.Interfaces;
using iPlanner.Presentation.Commands;
using iPlanner.Presentation.Commands.Reports;
using iPlanner.Presentation.Commands.Teams;
using iPlanner.Presentation.Interfaces;
using iPlanner.Presentation.Services.AppMediator.Base;

namespace iPlanner.Presentation.Services.AppMediator.MediatorMessages
{
    public class CloseFormMessageHandler : IMessageHandler<CloseFormMessage>
    {
        private readonly CloseFormCommand _command;
        public CloseFormMessageHandler(CloseFormCommand command)
        {
            _command = command;
        }
        public void Handle(CloseFormMessage message)
        {
            _command.SetMessage(message);
            _command.Execute();
        }

        public void Handle(MessageBase message)
        {
            CloseFormMessage? closeFormMessage = message.innerMessages?.OfType<CloseFormMessage>().FirstOrDefault();
            if (closeFormMessage != null)
            {
                Handle(closeFormMessage);
            }
        }
        public class CommandMessageHandler : IMessageHandler<CommandMessage>
        {
            public CommandMessageHandler()
            {
            }

            public void Handle(CommandMessage message)
            {
                if (message.CommandType == null)
                    throw new ArgumentNullException(nameof(message.CommandType));

                ICommand? command = Activator.CreateInstance(message.CommandType) as ICommand;

                if (command == null)
                    throw new InvalidOperationException($"No se pudo crear una instancia de ICommand desde {message.CommandType}");

                if (command is CommandInputMessageBase<CommandMessage>)
                {
                    ((CommandInputMessageBase<CommandMessage>)command).SetMessage(message);
                }

                command.Execute();
            }
        }

        public class ViewMessageHandler : IMessageHandler<ViewMessage>
        {
            private readonly InsertNewViewCommand _command;
            private readonly IMainWindow? _window;


            public ViewMessageHandler(InsertNewViewCommand command)
            {
                _command = command;
            }

            public void Handle(ViewMessage message)
            {
                _command.SetMessage(message);
                _command.Execute();
            }
        }


        public class TabMessageHandler : IMessageHandler<TabMessage>
        {
            private readonly SelectTabCommand _command;

            public TabMessageHandler(SelectTabCommand command)
            {
                _command = command;
            }

            public void Handle(TabMessage message)
            {
                _command.Document = message.Document;
                _command.Execute();
            }
        }

        public class TeamMessageHandler : IMessageHandler<TeamMessage>
        {
            private readonly AddTeamCommand _addTeamCommand;
            private readonly AddMemberCommand _addMemberCommand;
            private readonly CloseFormCommand _closeFormCommand;
            private readonly CloseFormMessageHandler _closeFormMessageHandler;
            private readonly RemoveTeamsCommand _removeTeamsCommand;
            private readonly ITeamService _teamService;

            public TeamMessageHandler()
            {
                _teamService = AppServices.GetService<ITeamService>();
                _addTeamCommand = new AddTeamCommand(_teamService);
                _addMemberCommand = new AddMemberCommand();
                _removeTeamsCommand = new RemoveTeamsCommand(_teamService);
                _closeFormCommand = new CloseFormCommand();
                _closeFormMessageHandler = new CloseFormMessageHandler(_closeFormCommand);
            }

            public void Handle(TeamMessage message)
            {
                if (message.CommandType == typeof(AddTeamCommand))
                {
                    _addTeamCommand.SetMessage(message);
                    _addTeamCommand.Execute();
                    _closeFormMessageHandler.Handle(message);
                }
                else if (message.CommandType == typeof(AddMemberCommand))
                {
                    _addMemberCommand.SetMessage(message);
                    _addMemberCommand?.Execute();
                }
                else if (message.CommandType == typeof(RemoveTeamsCommand))
                {
                    _removeTeamsCommand.SetMessage(message);
                    _removeTeamsCommand?.Execute();
                }

            }
        }


        public class ReportMessageHandler : IMessageHandler<ReportMessage>
        {
            private AddLocationReportCommand AddLocationsToReport;
            private RemoveLocationReportCommand RemoveLocationsFromReport;
            private CreateReportCommand CreateReportCommand;
            private UpdateReportCommand UpdateReportCommand;
            private CloseFormCommand CloseFormCommand;
            private DeleteReportCommand DeleteReportCommand;
            private CloseFormMessageHandler CloseFormMessageHandler;
            private IReportService _reportService;


            public ReportMessageHandler()
            {
                _reportService = AppServices.GetService<IReportService>();
                AddLocationsToReport = new AddLocationReportCommand();
                RemoveLocationsFromReport = new RemoveLocationReportCommand();
                CreateReportCommand = new CreateReportCommand(_reportService);
                UpdateReportCommand = new UpdateReportCommand(_reportService);
                DeleteReportCommand = new DeleteReportCommand(_reportService);
                CloseFormCommand = new CloseFormCommand();
                CloseFormMessageHandler = new CloseFormMessageHandler(CloseFormCommand);

            }

            public void Handle(ReportMessage message)
            {

                if (message.CommandType == typeof(AddLocationReportCommand))
                {
                    AddLocationsToReport.SetMessage(message);
                    AddLocationsToReport.Execute();
                }
                else if (message.CommandType == typeof(RemoveLocationReportCommand))
                {
                    RemoveLocationsFromReport.SetMessage(message);
                    RemoveLocationsFromReport.Execute();
                }
                else if (message.CommandType == typeof(CreateReportCommand))
                {
                    CreateReportCommand.SetMessage(message);
                    CreateReportCommand.Execute();
                    CloseFormMessageHandler.Handle(message);
                }
                else if (message.CommandType == typeof(DeleteReportCommand))
                {
                    DeleteReportCommand = new DeleteReportCommand(_reportService);
                    DeleteReportCommand.SetMessage(message);
                    DeleteReportCommand.Execute();

                }
                else if (message.CommandType == typeof(UpdateReportCommand))
                {
                    UpdateReportCommand = new UpdateReportCommand(_reportService);
                    UpdateReportCommand.SetMessage(message);
                    UpdateReportCommand.Execute();
                    CloseFormMessageHandler.Handle(message);
                }
            }
        }
    }

}
