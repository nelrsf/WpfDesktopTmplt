using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using WpfDesktopTmplt.Core.Application.DTO;
using WpfDesktopTmplt.Core.Application.Services;
using WpfDesktopTmplt.Presentation.ViewModels;

namespace WpfDesktopTmplt.Presentation.Controls
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