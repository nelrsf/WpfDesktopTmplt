using iPlanner.Core.Application.DTO;
using iPlanner.Core.Entities.Calendar;
using System.Collections.ObjectModel;

namespace iPlanner.Core.Application.Services
{
    public class CalendarService
    {
        private ObservableCollection<EventItem>? UpcomingEvents { get; set; }
        private ObservableCollection<EventItem>? DayEvents { get; set; }

        public void CalendarControl()
        {
            InitializeEvents();
        }

        public void InitializeEvents()
        {
            // Eventos próximos
            UpcomingEvents = new ObservableCollection<EventItem>
            {
                new EventItem {
                    Title = "Reunión con Cliente Importante",
                    DateTime = "Hoy, 15:00",
                    Type = "Reunión Externa",
                    Location = "Sala de Juntas Principal"
                },
                new EventItem {
                    Title = "Revisión de Propuesta Comercial",
                    DateTime = "Mañana, 10:00",
                    Type = "Reunión Interna",
                    Location = "Sala de Reuniones 2"
                },
                new EventItem {
                    Title = "Presentación Nuevo Producto",
                    DateTime = "20/11/2024, 09:00",
                    Type = "Presentación",
                    Location = "Auditorio"
                }
            };

            // Eventos del día
            DayEvents = new ObservableCollection<EventItem>
            {
                new EventItem {
                    Time = "09:00",
                    Title = "Reunión de Planificación",
                    Description = "Planificación semanal del equipo",
                    Type = "Reunión Interna",
                    Location = "Sala 1",
                    Participants = "Todo el equipo"
                },
                new EventItem {
                    Time = "11:00",
                    Title = "Presentación Nueva Propuesta",
                    Description = "Presentación al cliente ABC",
                    Type = "Reunión Externa",
                    Location = "Sala de Juntas",
                    Participants = "Equipo comercial, Cliente ABC"
                },
                new EventItem {
                    Time = "15:00",
                    Title = "Capacitación Producto",
                    Description = "Capacitación sobre nuevas características",
                    Type = "Formación",
                    Location = "Sala de Formación",
                    Participants = "Departamento de ventas"
                }
            };

        }


        public ObservableCollection<EventItemDTO> GetUpcomingEvents()
        {
            if (UpcomingEvents == null) return new ObservableCollection<EventItemDTO>();

            return new ObservableCollection<EventItemDTO>(
                    UpcomingEvents.Select(item => new EventItemDTO(item))
                );
        }

        public ObservableCollection<EventItemDTO> GetDayEvents()
        {
            if (DayEvents == null) return new ObservableCollection<EventItemDTO>();
            return new ObservableCollection<EventItemDTO>(
                    DayEvents.Select(item => new EventItemDTO(item))
                );
        }
    }
}
