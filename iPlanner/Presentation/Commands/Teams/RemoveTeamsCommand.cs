using iPlanner.Core.Application.DTO;
using iPlanner.Core.Application.Interfaces;
using iPlanner.Core.Entities.Teams;
using iPlanner.Infrastructure.Teams;
using System.Collections.ObjectModel;

namespace iPlanner.Presentation.Commands.Teams
{
    public class RemoveTeamsCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        private ITeamService _teamService;

        public RemoveTeamsCommand(ITeamService teamService) {
            _teamService = teamService;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            if (!CanExecute(parameter)) return;

            if(parameter is ObservableCollection<TeamDTO>)
            {
                ObservableCollection<TeamDTO> teamDTOs = (ObservableCollection<TeamDTO>)parameter;
                _teamService.RemoveTeams(teamDTOs);
            }
        }
    }
}
