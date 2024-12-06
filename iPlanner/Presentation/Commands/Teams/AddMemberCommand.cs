using iPlanner.Core.Application.DTO;
using iPlanner.Core.Application.Interfaces;
using System.Collections.ObjectModel;

namespace iPlanner.Presentation.Commands.Teams
{
    internal class AddMemberCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            if (!CanExecute(parameter)) return;

            if (!(parameter is TeamDTO)) return;

            TeamDTO team = (TeamDTO)parameter;
            if (team == null) return;


            team.Members.Insert(0, new TeamMemberDTO("", team.Members.Count.ToString()));

        }
    }
}
