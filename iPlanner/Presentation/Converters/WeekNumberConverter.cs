using System.Globalization;
using System.Windows.Data;

namespace iPlanner.Presentation.Converters
{
    public class WeekNumberConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int weekNumber)
            {
                return $"Semana {weekNumber}";
            }
            return value?.ToString() ?? string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
