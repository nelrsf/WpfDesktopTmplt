using iPlanner.Core.Application.DTO;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace iPlanner.Core.Application.Interfaces
{
    public interface ITeamService : INotifyPropertyChanged
    {
        public Task<ObservableCollection<TeamDTO>> GetAll();

        public void AddTeam(TeamDTO team);

        public void RemoveTeams(ObservableCollection<TeamDTO> teams);

    }
}
