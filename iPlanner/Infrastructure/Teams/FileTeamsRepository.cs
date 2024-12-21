using iPlanner.Core.Application.DTO;
using iPlanner.Core.Application.Interfaces.Repository;
using iPlanner.Infrastructure.Common;
using System.Collections.ObjectModel;

namespace iPlanner.Infrastructure.Teams
{
    public class FileTeamsRepository : ITeamsRepository
    {
        private FileService _fileService;
        private readonly string _teamsFilePath;
        private ObservableCollection<TeamDTO>? _teams;




        public FileTeamsRepository(FileService fileService)
        {
            _fileService = fileService;
            _teamsFilePath = _fileService.GetDataFilePath("Teams.json");
            _fileService.EnsureDirectoryExists(_teamsFilePath);
            LoadTeams();
        }


        private async void LoadTeams()
        {
            _teams = await GetTeams() ?? new ObservableCollection<TeamDTO>();
        }


        public async Task AddTeam(TeamDTO team)
        {
            if (_teams == null)
            {
                throw new ArgumentNullException(nameof(_teams));
            }

            await Task.Run(() =>
            {
                team.Id = IdGenerator.GenerateUUID();
                _teams.Add(team);
                SaveTeams();
            });
        }

        public async Task DeleteTeams(ObservableCollection<TeamDTO> teamsToRemove)
        {
            await Task.Run(() =>
            {
                if (_teams == null)
                {
                    throw new ArgumentNullException(nameof(_teams));
                }

                List<TeamDTO> tempTeams = new List<TeamDTO>();

                foreach (TeamDTO team in teamsToRemove)
                {
                    TeamDTO? teamToRemove = FindTeamById(team.Id);
                    if (teamToRemove != null)
                    {
                        tempTeams.Add(teamToRemove);
                    }
                }

                foreach (TeamDTO teamToRemove in tempTeams)
                {
                    _teams.Remove(teamToRemove);
                }

                SaveTeams();
            });
        }

        public async Task<ObservableCollection<TeamDTO>> GetTeams()
        {
            try
            {
                return await Task.Run(() =>
                {
                    return _fileService.LoadJsonData<ObservableCollection<TeamDTO>>(_teamsFilePath) ?? new ObservableCollection<TeamDTO>();
                });
            }
            catch (Exception ex)
            {
                throw new Exception("Error loading teams", ex);
            }

        }

        public async Task UpdateTeam(TeamDTO updatedTeam)
        {
            await Task.Run(() => { });
        }

        private void SaveTeams()
        {
            _fileService.SaveJsonData(_teamsFilePath, _teams);
        }

        private TeamDTO? FindTeamById(string? teamId)
        {
            if (_teams == null || teamId == null) return null;
            return _teams.FirstOrDefault(t => t.Id == teamId);
        }
    }
}
