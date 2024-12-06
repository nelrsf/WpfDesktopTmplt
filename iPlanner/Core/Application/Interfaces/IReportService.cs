using iPlanner.Core.Entities.Reports;

namespace iPlanner.Core.Application.Interfaces
{

    public interface IReportService
    {
        Task<List<Report>> GetReportsAsync();
    }

}
