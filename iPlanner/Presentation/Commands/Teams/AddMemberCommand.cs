using iPlanner.Core.Application.AppMediator.Base;
using iPlanner.Core.Application.DTO;
using iPlanner.Core.Application.Interfaces;
using iPlanner.Presentation.Services.MediatorMessages;

namespace iPlanner.Presentation.Commands.Teams
{
    public class AddMemberCommand : CommandInputMessageBase<TeamMessage>, ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute()
        {
            return true;
        }

        public void Execute()
        {
            if (!CanExecute()) return;
            if (message == null) return;
            TeamMessage teamMessage = message;
            if (teamMessage == null) return;

            TeamDTO? team = teamMessage.TeamToCreate;

            if (team == null) return;


            team?.Members?.Insert(0, new TeamMemberDTO("", ""));

        }
    }
}
