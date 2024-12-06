using iPlanner.Core.Application.Interfaces;

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
        }
    }
}
