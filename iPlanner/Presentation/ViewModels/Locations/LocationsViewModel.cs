using iPlanner.Core.Application.DTO;
using iPlanner.Core.Application.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace iPlanner.Presentation.ViewModels.Locations
{
    public class LocationsViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<LocationItemDTO> Locations { get; set; }
        private ObservableCollection<LocationItemDTO> _filteredLocations;
        public ObservableCollection<LocationItemDTO> FilteredLocations
        {
            get => _filteredLocations;
            set
            {
                _filteredLocations = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FilteredLocations)));
            }
        }

        private bool _isSelectionMode = true;
        private string? _searchText;
        private ILocationService _locationService;
        private bool _isLoading;
        private bool _isSearching;

        public bool IsSearching
        {
            get => _isSearching;
            set
            {
                _isSearching = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsSearching)));
            }
        }

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsLoading)));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public LocationsViewModel(ILocationService locationService)
        {
            Locations = new ObservableCollection<LocationItemDTO>();
            _locationService = locationService;
            InitializeLocations();
        }
        public string? SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SearchText)));
            }
        }
        public bool IsSelectionMode
        {
            get => _isSelectionMode;
            set
            {
                _isSelectionMode = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsSelectionMode)));
            }
        }


        private async void InitializeLocations()
        {
            FilteredLocations = new ObservableCollection<LocationItemDTO>();
            IsLoading = true;
            await Task.Factory.StartNew(async () =>
            {
                var locationItems = await _locationService.GetLocations();
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Locations.Clear();
                    foreach (var item in locationItems)
                    {
                        Locations.Add(item);
                    }
                    FilteredLocations.Clear();
                    FilteredLocations = new ObservableCollection<LocationItemDTO>(Locations);
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FilteredLocations)));
                    IsLoading = false;
                });
            });

        }

        public void AddLocation()
        {
            MessageBox.Show("Agregar ubicación");
        }

        public void EditLocation()
        {
            MessageBox.Show("Editar ubicación");
        }

        public void DeleteLocation()
        {
            MessageBox.Show("Eliminar ubicación");
        }

        public void ViewLocation()
        {
            MessageBox.Show("Ver ubicación");
        }

        public void FillterLocations(string text)
        {
            var filteredLocations = Locations.Where(l => l.Name == text);
            Locations.Clear();
            foreach (var item in filteredLocations)
            {
                Locations.Add(item);
            }
        }
    }
}