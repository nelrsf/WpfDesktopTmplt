using iPlanner.Core.Entities.Teams;

namespace iPlanner.Core.Entities.Reports
{
    public class ReportFilter
    {
        public DateTime? DateInit { get; set; }

        public DateTime? DateEnd { get; set; }

        public Team? Team { get; set; }

        public bool CheckTime()
        {
            if (DateInit == null) return true;
            if (DateEnd == null) return true;
            return DateInit < DateEnd;
        }

        internal List<Report> FilterReports(List<Report> reports)
        {
            return reports
                .Where(r => r.Date >= DateInit || DateInit == null)
                .Where(r => r.Date <= DateEnd || DateEnd == null)
                .Where(r => Team == null ||  r?.Team?.Id == Team?.Id )
                .ToList();
        }
    }
}
