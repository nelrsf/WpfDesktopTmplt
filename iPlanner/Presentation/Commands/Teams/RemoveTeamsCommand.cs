using iPlanner.Core.Application.DTO;
using iPlanner.Core.Application.Interfaces;
using System.Collections.ObjectModel;

namespace iPlanner.Presentation.Commands.Teams
{
    public class RemoveTeamsCommand : ICommand<ObservableCollection<TeamDTO>>
    {
        public event EventHandler? CanExecuteChanged;
        private ITeamService _teamService;

        public RemoveTeamsCommand(ITeamService teamService)
        {
            _teamService = teamService;
        }

        public bool CanExecute(ObservableCollection<TeamDTO>? parameter)
        {
            return true;
        }

        public void Execute(ObservableCollection<TeamDTO>? teamDTOs)
        {
            if (!CanExecute(teamDTOs)) return;
            _teamService.RemoveTeams(teamDTOs);

        }
    }
}
