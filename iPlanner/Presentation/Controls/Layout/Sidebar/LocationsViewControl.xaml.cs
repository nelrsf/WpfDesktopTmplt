using iPlanner.Core.Application.DTO;
using iPlanner.Core.Application.Services;
using iPlanner.Presentation.Services;
using iPlanner.Presentation.ViewModels.Locations;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace iPlanner.Presentation.Controls.Sidebar
{
    public class IconNameToResourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string iconName)
            {
                return Application.Current.Resources[iconName];
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public partial class LocationsViewControl : UserControl
    {
        private DragNDropManager<List<LocationItemDTO>> _dragManager;
        private LocationsViewModel _viewModel;
        private SelectionManager<LocationItemDTO> _selectionManager;


        public LocationsViewControl()
        {
            InitializeComponent();

            _viewModel = new LocationsViewModel(AppServices.GetService<ILocationService>());
            _dragManager = new DragNDropManager<List<LocationItemDTO>>();
            DataContext = _viewModel;
            _selectionManager = new SelectionManager<LocationItemDTO>();
        }

        private void OnAddLocation(object sender, RoutedEventArgs e)
        {
            _viewModel.AddLocation();
        }

        private void OnEditLocation(object sender, RoutedEventArgs e)
        {
            _viewModel.EditLocation();
        }

        private void OnDeleteLocation(object sender, RoutedEventArgs e)
        {
            _viewModel.DeleteLocation();
        }

        private void OnViewLocation(object sender, RoutedEventArgs e)
        {
            _viewModel.ViewLocation();
        }

        private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
        {
            var searchText = (sender as TextBox)?.Text;
            _viewModel.SearchText = searchText;
        }

        private void OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _dragManager.DataTransfer = (List<LocationItemDTO>)_selectionManager.SelectedItems;
            _dragManager.OnMouseDown(sender, e);
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            _dragManager.OnMouseMove(sender, e);
        }

        private void OnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _dragManager.OnMouseUp(sender, e);
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox)
            {
                CheckBox checkBox = sender as CheckBox;
                if (checkBox.DataContext is LocationItemDTO locationItemDTO)
                {
                    _selectionManager.Add(locationItemDTO);
                }
            }
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox)
            {
                CheckBox checkBox = sender as CheckBox;
                if (checkBox.DataContext is LocationItemDTO locationItemDTO)
                {
                    _selectionManager.Remove(locationItemDTO);
                }
            }
        }
    }
}