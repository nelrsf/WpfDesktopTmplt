namespace iPlanner.Core.Entities.Reports
{
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
