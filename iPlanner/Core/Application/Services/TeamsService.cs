using iPlanner.Core.Application.DTO;
using iPlanner.Core.Application.Interfaces;
using iPlanner.Core.Application.Interfaces.Repository;

namespace iPlanner.Core.Application.Services
{
    public class TeamsService : ITeamService
    {
        private ITeamsRepository _teamsRepository;

        public TeamsService(ITeamsRepository teamsRepository)
        {
            _teamsRepository = teamsRepository;

            InitializeTeams();
        }

        private async void InitializeTeams()
        {
            ICollection<TeamDTO> teams = await _teamsRepository.GetTeams();
        }


        public async Task<ICollection<TeamDTO>> GetAll()
        {
            return await _teamsRepository.GetTeams();
        }

        public async void AddTeam(TeamDTO team)
        {
            await _teamsRepository.AddTeam(team);
        }

        public async void RemoveTeams(ICollection<TeamDTO> teamsToRemove)
        {
            await _teamsRepository.DeleteTeams(teamsToRemove);
        }

    }
}