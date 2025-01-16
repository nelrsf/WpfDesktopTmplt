using iPlanner.Core.Entities.Locations;
using iPlanner.Core.Entities.Teams;

namespace iPlanner.Core.Entities.Reports
{
    public class Report
    {
        private double _totalHours;
        public string? ReportId { get; set; }
        public Team? Team { get; set; }

        private DateTime? _date;

        public double TotalHours
        {
            get
            {
                if (TimeInit.HasValue && TimeEnd.HasValue)
                {
                    return (TimeEnd.Value - TimeInit.Value).TotalHours;
                }
                return 0;
            }
        }

        public DateTime? Date
        {
            get
            {
                return _date;
            }
            set
            {
                _date = value;
                ReportScheduleUpdater.UpdateScheduleByDate(this);
            }
        }

        public TimeSpan? TimeInit { get; set; }

        public TimeSpan? TimeEnd { get; set; }

        public ICollection<Activity>? Activities { get; set; }

        public Report() { }
    }

    public class Activity
    {
        public ICollection<LocationItem> Locations { get; set; }
        public string? Description { get; set; }
        public Activity()
        {
            Locations = new List<LocationItem>();
        }
    }
}
