using iPlanner.Core.Application.DTO;

namespace iPlanner.Core.Application.Interfaces.Repository
{
    public interface IReportRepository
    {
        public Task<List<ReportDTO>> GetReports();
        public Task AddReport(ReportDTO report);
        public Task UpdateReport(ReportDTO updatedReport);
        public Task DeleteReport(ReportDTO report);

    }
}
