using iPlanner.Core.Application.DTO;
using iPlanner.Core.Application.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace iPlanner.Presentation.ViewModels
{
    public class CalendarViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private ObservableCollection<EventItemDTO> _upcomingEvents;
        private ObservableCollection<EventItemDTO> _dayEvents;

        public ObservableCollection<EventItemDTO> UpcomingEvents
        {
            get => _upcomingEvents;
            set
            {
                if (_upcomingEvents != value)
                {
                    _upcomingEvents = value;
                    OnPropertyChanged(nameof(UpcomingEvents));
                }
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<EventItemDTO> DayEvents
        {
            get => _dayEvents;
            set
            {
                if (_dayEvents != value)
                {
                    _dayEvents = value;
                    OnPropertyChanged(nameof(DayEvents));
                }
            }
        }

        private CalendarService _calendarService;
        public CalendarViewModel(CalendarService calendarService)
        {
            _upcomingEvents = new ObservableCollection<EventItemDTO>();
            _dayEvents = new ObservableCollection<EventItemDTO>();
            _calendarService = calendarService;
            InitializeEvents();
        }


        public void InitializeEvents()
        {
            _calendarService.InitializeEvents();
            var upcomingEvents = _calendarService.GetUpcomingEvents();
            var dayEvents = _calendarService.GetDayEvents();

            UpcomingEvents.Clear();
            DayEvents.Clear();

            foreach (var upcEvt in upcomingEvents)
            {
                _upcomingEvents.Add(upcEvt);
            }

            foreach (var dayEvt in dayEvents)
            {
                _dayEvents.Add(dayEvt);
            }
        }


    }
}
