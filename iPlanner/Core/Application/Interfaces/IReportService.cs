using iPlanner.Core.Application.DTO;

namespace iPlanner.Core.Application.Interfaces
{
    public interface IReportService
    {

        Task<List<ReportDTO>> GetReportsAsync();


        Task AddReportAsync(ReportDTO report);


        Task UpdateReportAsync(ReportDTO report);


        Task DeleteReportAsync(ReportDTO report);

        ReportDTO InitializeNewReport();
        ReportDTO RefreshReportDate(ReportDTO dto);
    }
}