using iPlanner.Core.Application.DTO;
using System.Collections.ObjectModel;

namespace iPlanner.Core.Application.Interfaces.Repository
{
    public interface ITeamsRepository
    {
        public Task<ObservableCollection<TeamDTO>> GetTeams();
        public Task AddTeam(TeamDTO team);
        public Task UpdateTeam(TeamDTO updatedTeam);
        public Task DeleteTeams(ObservableCollection<TeamDTO> teams);
    }
}
