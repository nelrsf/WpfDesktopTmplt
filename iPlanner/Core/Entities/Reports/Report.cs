using iPlanner.Core.Entities.Locations;
using iPlanner.Core.Entities.Teams;
using System.Collections.ObjectModel;

namespace iPlanner.Core.Entities.Reports
{
    public class Report
    {
        public string? ReportId { get; set; }
        public Team? Team { get; set; }

        private DateTime? _date;
        public DateTime? Date
        {
            get
            {
                return _date;
            }
            set
            {
                _date = value;
                UpdateScheduleByDate();
            }
        }

        public TimeSpan? TimeInit { get; set; }

        public TimeSpan? TimeEnd { get; set; }

        public ObservableCollection<Activity>? Activities { get; set; }

        public Report() { }

        private void UpdateScheduleByDate()
        {
            if (Date == null) return;
            switch (Date.Value.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    TimeInit = TimeSpan.FromHours(8);
                    TimeEnd = TimeSpan.FromHours(17);
                    break;
                case DayOfWeek.Tuesday:
                    TimeInit = TimeSpan.FromHours(8);
                    TimeEnd = TimeSpan.FromHours(17);
                    break;
                case DayOfWeek.Wednesday:
                    TimeInit = TimeSpan.FromHours(7);
                    TimeEnd = TimeSpan.FromHours(17);
                    break;
                case DayOfWeek.Thursday:
                    TimeInit = TimeSpan.FromHours(7);
                    TimeEnd = TimeSpan.FromHours(17);
                    break;
                case DayOfWeek.Friday:
                    TimeInit = TimeSpan.FromHours(7);
                    TimeEnd = TimeSpan.FromHours(15);
                    break;

            }
        }
    }

    public class Activity
    {
        public ObservableCollection<LocationItem> Locations { get; set; }
        public string? Description { get; set; }
        public Activity()
        {
            Locations = new ObservableCollection<LocationItem>();
        }

    }

}
