using iPlanner.Core.Application.DTO;
using iPlanner.Core.Application.Interfaces;
using iPlanner.Presentation.Interfaces;
using iPlanner.Presentation.Services.AppMediator.Base;
using iPlanner.Presentation.Services.AppMediator.MediatorMessages;
using System.Windows;

namespace iPlanner.Presentation.Commands.Teams
{
    public class AddTeamCommand : CommandInputMessageBase<TeamMessage>, ICommand
    {
        public event EventHandler? CanExecuteChanged;
        private ITeamService _teamService;

        public AddTeamCommand(ITeamService teamService)
        {
            _teamService = teamService;
        }

        public bool CanExecute()
        {
            return true;
        }

        public void Execute()
        {

            if (!CanExecute()) return;
            if (message == null) return;
            TeamDTO? team = message.TeamToCreate;
            if (team == null)
            {
                throw new Exception("Team is null");
            }
            try
            {
                _teamService.AddTeam(team);
                NotifyMessage(true);
            }
            catch (Exception e)
            {
                NotifyMessage(false);
            }
        }
    }
}
