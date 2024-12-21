using iPlanner.Core.Application.Services;
using iPlanner.Presentation.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace iPlanner.Presentation.Controls
{
    public partial class CalendarControl : UserControl
    {
        private CalendarViewModel calendarViewModel { get; set; }

        public CalendarControl()
        {

            InitializeComponent();
            calendarViewModel = new CalendarViewModel(new CalendarService());
            UpdateSelectedDate(DateTime.Now);
            DataContext = calendarViewModel;
        }



        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            if (mainCalendar.SelectedDate.HasValue)
            {
                UpdateSelectedDate(mainCalendar.SelectedDate.Value);
            }
        }

        private void UpdateSelectedDate(DateTime date)
        {
            selectedDateText.Text = date.ToLongDateString();
            // Aquí podrías cargar los eventos específicos para la fecha seleccionada
        }

        private void AddEvent_Click(object sender, RoutedEventArgs e)
        {
            // Aquí iría la lógica para agregar un nuevo evento
            MessageBox.Show("Funcionalidad de agregar evento próximamente", "Nuevo Evento");
        }
    }
}