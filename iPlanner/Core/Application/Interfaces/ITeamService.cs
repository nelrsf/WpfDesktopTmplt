using iPlanner.Core.Application.DTO;
using System.Collections.ObjectModel;

namespace iPlanner.Core.Application.Interfaces
{
    public interface ITeamService
    {
        public ObservableCollection<TeamDTO> LoadData();

        public void AddTeam(TeamDTO team);
    }
}
