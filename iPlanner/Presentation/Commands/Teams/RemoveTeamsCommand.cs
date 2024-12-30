using iPlanner.Core.Application.AppMediator.Base;
using iPlanner.Core.Application.DTO;
using iPlanner.Core.Application.Interfaces;
using iPlanner.Presentation.Services.MediatorMessages;
using System.Collections.ObjectModel;

namespace iPlanner.Presentation.Commands.Teams
{
    public class RemoveTeamsCommand : CommandInputMessageBase<TeamMessage>, ICommand
    {
        public event EventHandler? CanExecuteChanged;
        private ITeamService _teamService;

        public RemoveTeamsCommand(ITeamService teamService)
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
            ObservableCollection<TeamDTO>? teamDTOs = message?.TeamsToRemove;
            if (teamDTOs == null) return;
            _teamService.RemoveTeams(teamDTOs);

        }
    }
}
