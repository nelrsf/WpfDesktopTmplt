using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace iPlanner.Presentation.Controls.Common
{
    public partial class TimePickerControl : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public static readonly DependencyProperty TimeValueProperty =
            DependencyProperty.Register(
                nameof(TimeValue),
                typeof(TimeSpan?),
                typeof(TimePickerControl),
                new FrameworkPropertyMetadata(
                    null,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    OnTimeValueChanged));

        public TimeSpan? TimeValue
        {
            get => (TimeSpan?)GetValue(TimeValueProperty);
            set => SetValue(TimeValueProperty, value);
        }

        public ObservableCollection<int> Hours { get; private set; }
        public ObservableCollection<int> Minutes { get; private set; }

        public TimePickerControl()
        {
            InitializeComponent();
            Hours = new ObservableCollection<int>(Enumerable.Range(0, 24));
            Minutes = new ObservableCollection<int>(Enumerable.Range(0, 60));

            HourComboBox.ItemsSource = Hours;
            MinuteComboBox.ItemsSource = Minutes;

            // Inicializar con valores por defecto
            if (!TimeValue.HasValue)
            {
                TimeValue = new TimeSpan(0, 0, 0);
            }
        }

        private static void OnTimeValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TimePickerControl control)
            {
                var time = (TimeSpan?)e.NewValue;
                control.UpdateSelectedTime(time);
            }
        }

        private void UpdateSelectedTime(TimeSpan? time)
        {
            if (HourComboBox != null && MinuteComboBox != null)
            {
                if (time.HasValue)
                {
                    HourComboBox.SelectedItem = time.Value.Hours;
                    MinuteComboBox.SelectedItem = time.Value.Minutes;
                }
                else
                {
                    HourComboBox.SelectedItem = 0;
                    MinuteComboBox.SelectedItem = 0;
                }
            }
        }

        private void HourComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsLoaded) UpdateTimeValue();
        }

        private void MinuteComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsLoaded) UpdateTimeValue();
        }

        private void UpdateTimeValue()
        {
            if (HourComboBox?.SelectedItem is int hours &&
                MinuteComboBox?.SelectedItem is int minutes)
            {
                TimeValue = new TimeSpan(hours, minutes, 0);
                OnPropertyChanged(nameof(TimeValue));
            }
        }

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}