using iPlanner.Core.Application.DTO;
using iPlanner.Core.Application.Interfaces;
using iPlanner.Core.Application.Interfaces.Repository;
using iPlanner.Core.Entities.Reports;

namespace iPlanner.Core.Application.Services
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _reportsRepository;
        private readonly IMapper<ReportDTO, Report> _reportMapper;

        public ReportService(IReportRepository repository, IMapper<ReportDTO, Report> reportMapper)
        {
            _reportsRepository = repository;
            _reportMapper = reportMapper;
        }

        public async Task<List<ReportDTO>> GetReportsAsync()
        {
            return await _reportsRepository.GetReports();
        }

        public async Task AddReportAsync(ReportDTO report)
        {
            await _reportsRepository.AddReport(report);
        }

        public async Task DeleteReportAsync(ReportDTO report)
        {
            await _reportsRepository.DeleteReport(report);
        }

        public async Task UpdateReportAsync(ReportDTO updatedReport)
        {
            await _reportsRepository.UpdateReport(updatedReport);
        }

        public ReportDTO InitializeNewReport()
        {
            return new ReportDTO();
        }

        public ReportDTO RefreshReportDate(ReportDTO dto)
        {
            Report report = _reportMapper.ToEntity(dto);
            report.Date = dto.Date;
            dto = _reportMapper.ToDTO(report);
            return dto;
        }
    }
}

