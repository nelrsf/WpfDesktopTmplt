namespace iPlanner.Core.Entities.Reports
{
    public class ReportFilter
    {
        public DateTime? Date {  get; set; }

        public TimeSpan? TimeInit { get; set; }

        public TimeSpan? TimeEnd { get; set; }

        public bool CheckTime()
        {
            if(TimeEnd == null) return false;
            if(TimeInit ==  null) return false;
            return TimeInit < TimeEnd;
        }

    }
}
