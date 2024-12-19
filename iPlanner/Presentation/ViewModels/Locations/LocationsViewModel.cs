using iPlanner.Core.Application.DTO;
using iPlanner.Core.Application.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace iPlanner.Presentation.ViewModels.Locations
{
    public class LocationsViewModel : INotifyPropertyChanged
    {
        private bool _isSelectionMode;
        private string? _searchText;
        private ILocationService _locationService;
        private bool _isLoading;
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

        public ObservableCollection<LocationItemDTO> Locations { get; set; }


        private async void InitializeLocations()
        {
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
    }
}