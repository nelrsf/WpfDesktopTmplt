using WpfDesktopTmplt.Core.Entities.Calendar;

namespace WpfDesktopTmplt.Core.Application.DTO
{
    public class EventItemDTO
    {
        public string Title { get; set; }
        public string DateTime { get; set; }
        public string Time { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Location { get; set; }
        public string Participants { get; set; }

        public EventItemDTO(EventItem eventItem)
        {
            Title = eventItem.Title;
            DateTime = eventItem.DateTime;
            Time = eventItem.Time;
            Description = eventItem.Description;
            Type = eventItem.Type;
            Location = eventItem.Location;
            Participants = eventItem.Participants;

        }
    }
}
