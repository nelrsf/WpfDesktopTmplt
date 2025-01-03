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

    public static class ReportScheduleUpdater
    {
        private static readonly Dictionary<DayOfWeek, (TimeSpan Start, TimeSpan End)> Schedule = new()
        {
            { DayOfWeek.Monday, (TimeSpan.FromHours(8), TimeSpan.FromHours(17)) },
            { DayOfWeek.Tuesday, (TimeSpan.FromHours(8), TimeSpan.FromHours(17)) },
            { DayOfWeek.Wednesday, (TimeSpan.FromHours(7), TimeSpan.FromHours(17)) },
            { DayOfWeek.Thursday, (TimeSpan.FromHours(7), TimeSpan.FromHours(17)) },
            { DayOfWeek.Friday, (TimeSpan.FromHours(7), TimeSpan.FromHours(15)) }
        };

        public static double GetRequieredTotalHours()
        {
            return Schedule.Sum(day => (day.Value.End - day.Value.Start).TotalHours);
        }

        public static void UpdateScheduleByDate(Report report)
        {
            if (report.Date == null) return;

            var dayOfWeek = report.Date.Value.DayOfWeek;
            if (Schedule.ContainsKey(dayOfWeek))
            {
                report.TimeInit = Schedule[dayOfWeek].Start;
                report.TimeEnd = Schedule[dayOfWeek].End;
            }
        }
    }
}
