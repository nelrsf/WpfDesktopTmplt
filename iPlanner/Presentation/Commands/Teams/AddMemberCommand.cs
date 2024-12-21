using iPlanner.Core.Application.DTO;
using iPlanner.Core.Application.Interfaces;

namespace iPlanner.Presentation.Commands.Teams
{
    public class AddMemberCommand : ICommand<TeamDTO>
    {
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(TeamDTO? parameter)
        {
            return true;
        }

        public void Execute(TeamDTO? team)
        {
            if (!CanExecute(team)) return;


            if (team == null) return;


            team.Members.Insert(0, new TeamMemberDTO("", team.Members.Count.ToString()));

        }
    }
}
