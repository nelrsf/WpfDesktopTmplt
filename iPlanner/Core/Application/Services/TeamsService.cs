using iPlanner.Core.Application.DTO;
using iPlanner.Core.Application.Interfaces;
using iPlanner.Core.Application.Interfaces.Repository;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace iPlanner.Core.Application.Services
{
    public class TeamsService : ITeamService, INotifyPropertyChanged
    {
        private ITeamsRepository _teamsRepository;
        public event PropertyChangedEventHandler? PropertyChanged;

        public TeamsService(ITeamsRepository teamsRepository)
        {
            _teamsRepository = teamsRepository;

            InitializeTeams();
        }

        private async void InitializeTeams()
        {
            ObservableCollection<TeamDTO> teams = await _teamsRepository.GetTeams();
            teams.CollectionChanged += Teams_CollectionChanged;
        }

        private void Teams_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged();
        }

        public async Task<ObservableCollection<TeamDTO>> GetAll()
        {
            return await _teamsRepository.GetTeams();
        }

        public async void AddTeam(TeamDTO team)
        {
            await _teamsRepository.AddTeam(team);
            OnPropertyChanged();
        }

        public async void RemoveTeams(ObservableCollection<TeamDTO> teamsToRemove)
        {
            await _teamsRepository.DeleteTeams(teamsToRemove);
            OnPropertyChanged();
        }

        private void OnPropertyChanged()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Teams"));
        }
    }
}