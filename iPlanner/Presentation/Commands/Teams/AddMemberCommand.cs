using iPlanner.Core.Application.DTO.Teams;
using iPlanner.Presentation.Interfaces;
using iPlanner.Presentation.Services.AppMediator.Base;
using iPlanner.Presentation.Services.AppMediator.MediatorMessages;
using System.Collections.ObjectModel;

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

            if(team.Members is ObservableCollection<TeamMemberDTO> members)
            {
                members.Insert(0, new TeamMemberDTO("", ""));
            } else
            {
                team.Members?.Add(new TeamMemberDTO("", ""));
            }

        }
    }
}
