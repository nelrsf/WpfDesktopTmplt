using iPlanner.Core.Application.DTO;
using iPlanner.Core.Application.Interfaces;
using iPlanner.Presentation.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace iPlanner.Presentation.ViewModels
{
    public class TeamsViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<TeamDTO>? _teams;
        private ObservableCollection<TeamDTO>? _selectedTeams;
        private ITeamService _teamsService;
        private IMediator _appMediatorService;
        private string? _searchText;

        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<TeamDTO>? Teams
        {
            get => _teams;
            set
            {
                _teams = value;
                OnPropertyChanged(nameof(Teams));
            }
        }

        public ObservableCollection<TeamDTO>? SelectedTeams
        {
            get => _selectedTeams;
            set
            {
                _selectedTeams = value;
                OnPropertyChanged(nameof(SelectedTeams));
            }
        }

        public string? SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged(nameof(SearchText));
                FilterTeams();
            }
        }

        public TeamsViewModel(ITeamService teamsService, IMediator mediator)
        {
            SelectedTeams = new ObservableCollection<TeamDTO>();
            Teams = new ObservableCollection<TeamDTO>();
            _teamsService = teamsService;
            _appMediatorService = mediator;
            LoadData();
        }

        private void LoadData()
        {
            Teams = _teamsService.LoadData();
        }

        private void FilterTeams()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                LoadData();
                return;
            }

            if(Teams==null) return;

            var filteredTeams = Teams.Where(t =>
                t.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                t.Description.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                t.Members.Any(m => m.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase))
            ).ToList();

            Teams.Clear();
            foreach (var team in filteredTeams)
            {
                Teams.Add(team);
            }
        }

        public void AddTeam()
        {
            _appMediatorService.Notify(this, CommandType.InsertNewView, ControlFactory.TEAMS_FORM_CONTROL);
        }

        public void EditTeam()
        {
            if (SelectedTeams != null)
            {
                MessageBox.Show("Funcionalidad de editar equipo en desarrollo");
            }
        }

        public void DeleteTeam()
        {
            if (SelectedTeams != null)
            {
                MessageBox.Show("Funcionalidad de eliminar equipo en desarrollo");
            }
        }

        public void UpdateSelectedTeams(ObservableCollection<TeamDTO> selectedTeams)
        {
            if (SelectedTeams == null) return;
            SelectedTeams.Clear();
            foreach (var team in selectedTeams)
            {
                SelectedTeams.Add(team);
            }
        }

        public void ViewTeamDetails()
        {
            if (SelectedTeams != null)
            {
                MessageBox.Show("Funcionalidad de ver detalles en desarrollo");
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
