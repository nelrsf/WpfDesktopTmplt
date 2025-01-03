using iPlanner.Core.Application.DTO.Teams;
using iPlanner.Core.Application.Services;
using System.Collections.ObjectModel;
using System.Windows.Threading;

namespace iPlanner.Presentation.ViewModels
{
    public class TeamScheduleViewModel : ViewModelBase
    {
        private readonly ITeamScheduleService _teamScheduleService;
        private ObservableCollection<ScheduleTeamItemDTO> _teamItems;
        private ObservableCollection<ConflictItemDTO> _conflictItems;
        private ObservableCollection<int> _years;
        private ObservableCollection<int> _weeks;
        private int _selectedYear;
        private int _selectedWeek;
        private bool _hasConflicts;

        public TeamScheduleViewModel(ITeamScheduleService teamScheduleService)
        {
            _teamScheduleService = teamScheduleService;
        }

        public async void Initialize()
        {
            await InitializeCollectionsAsync().ConfigureAwait(false);
            LoadData();
        }

        public ObservableCollection<ScheduleTeamItemDTO> TeamItems
        {
            get => _teamItems;
            set
            {
                _teamItems = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ConflictItemDTO> ConflictItems
        {
            get => _conflictItems;
            set
            {
                _conflictItems = value;
                OnPropertyChanged();
                UpdateHasConflicts();
            }
        }

        public ObservableCollection<int> Years
        {
            get => _years;
            set
            {
                _years = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<int> Weeks
        {
            get => _weeks;
            set
            {
                _weeks = value;
                OnPropertyChanged();
            }
        }

        public int SelectedYear
        {
            get => _selectedYear;
            set
            {
                _selectedYear = value;
                OnPropertyChanged();
                LoadData();
            }
        }

        public int SelectedWeek
        {
            get => _selectedWeek;
            set
            {
                _selectedWeek = value;
                OnPropertyChanged();
                LoadData();
            }
        }

        public bool HasConflicts
        {
            get => _hasConflicts;
            set
            {
                _hasConflicts = value;
                OnPropertyChanged();
            }
        }

        private async Task InitializeCollectionsAsync()
        {
            try
            {
                TeamItems = new ObservableCollection<ScheduleTeamItemDTO>();
                ConflictItems = new ObservableCollection<ConflictItemDTO>();
                Years = new ObservableCollection<int>(await _teamScheduleService.GetAvailableYears().ConfigureAwait(false));
                Weeks = new ObservableCollection<int>(await _teamScheduleService.GetAvailableWeeks().ConfigureAwait(false));
                Dispatcher.CurrentDispatcher.Invoke(() =>
                {
                    SelectedYear = Years.Max();
                    SelectedWeek = Weeks.Max();
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al inicializar las colecciones: {ex.Message}");
            }
        }

        private void UpdateHasConflicts()
        {
            HasConflicts = ConflictItems != null && ConflictItems.Count > 0;
        }

        private async void LoadData()
        {
            var scheduleData = await _teamScheduleService.GetScheduleData(SelectedYear, SelectedWeek);
            TeamItems = new ObservableCollection<ScheduleTeamItemDTO>(scheduleData.TeamItems);
            ConflictItems = new ObservableCollection<ConflictItemDTO>(scheduleData.ConflictItems);
        }
    }
}
