using iPlanner.Core.Application.DTO;
using iPlanner.Core.Application.DTO.Reports;

namespace iPlanner.Core.Application.Interfaces
{
    public interface IReportService
    {

        Task<List<ReportDTO>> GetReportsAsync();

        Task<List<ReportDTO>> GetReportsAsyncByFilter(ReportFilterDTO filterDTO);

        Task AddReportAsync(ReportDTO report);


        Task UpdateReportAsync(ReportDTO report);


        Task DeleteReportAsync(ReportDTO report);

        ReportDTO InitializeNewReport();
        ReportDTO RefreshReportDate(ReportDTO dto);
    }
}