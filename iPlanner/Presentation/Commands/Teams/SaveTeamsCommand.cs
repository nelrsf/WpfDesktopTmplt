using iPlanner.Core.Application.DTO;
using iPlanner.Core.Application.Interfaces;

namespace iPlanner.Presentation.Commands.Teams
{
    public class SaveTeamsCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        private ITeamService _teamService;

        public SaveTeamsCommand(ITeamService teamService) { 
            _teamService = teamService;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            if(parameter is TeamDTO)
            {
                TeamDTO team = (TeamDTO)parameter;
                _teamService.AddTeam(team);
                
            }
        }
    }
}
