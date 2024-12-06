namespace iPlanner.Core.Entities.Reports
{
    public class Report
    {
        public string? Team { get; set; }
        public string? Leader { get; set; }
        public List<Activity>? Activities { get; set; }
    }

    public class Activity
    {
        public string? Date { get; set; }
        public string? Description { get; set; }
        public List<Worker>? Workers { get; set; }
    }

    public class Worker
    {
        public string? Name { get; set; }
        public string? Init { get; set; }
        public string? End { get; set; }
    }

}
