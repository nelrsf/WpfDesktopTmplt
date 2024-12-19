using iPlanner.Core.Application.DTO;
using iPlanner.Core.Application.Interfaces;
using System.Windows;

namespace iPlanner.Presentation.Commands.Teams
{
    public class AddTeamCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        private ITeamService _teamService;

        public AddTeamCommand(ITeamService teamService) { 
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
                MessageBox.Show("Frente de trabajo agregado correctamente");
                
            }
        }
    }
}
