using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using iPlanner.Infrastructure.Locations;
using iPlanner.Presentation.ViewModels;

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
        private LocationsViewModel _viewModel;

        public LocationsViewControl()
        {
            InitializeComponent();
            _viewModel = new LocationsViewModel(new LocationService());
            DataContext = _viewModel;
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

    }
}