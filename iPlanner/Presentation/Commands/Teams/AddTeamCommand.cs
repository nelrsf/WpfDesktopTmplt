using iPlanner.Core.Application.DTO;
using iPlanner.Core.Application.Interfaces;
using System.Windows;

namespace iPlanner.Presentation.Commands.Teams
{
    public class AddTeamCommand : ICommand<TeamDTO>
    {
        public event EventHandler? CanExecuteChanged;
        private ITeamService _teamService;

        public AddTeamCommand(ITeamService teamService)
        {
            _teamService = teamService;
        }

        public bool CanExecute(TeamDTO? parameter)
        {
            return true;
        }

        public void Execute(TeamDTO? team)
        {

            _teamService.AddTeam(team);
            MessageBox.Show("Frente de trabajo agregado correctamente");


        }
    }
}
