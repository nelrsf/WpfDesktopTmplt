using iPlanner.Core.Application.Interfaces;
using iPlanner.Presentation.Commands;
using iPlanner.Presentation.Commands.Teams;
using iPlanner.Presentation.Interfaces;
using iPlanner.Presentation.Services.AppMediator.MediatorMessages;

namespace iPlanner.Presentation.Services.AppMediator.MediatorHandlers
{
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

}
