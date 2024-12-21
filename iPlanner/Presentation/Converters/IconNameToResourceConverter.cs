using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace iPlanner.Presentation.Converters
{
    public class IconNameToResourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string iconName)
            {
                return Application.Current.FindResource(iconName);
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}