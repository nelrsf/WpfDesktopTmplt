using iPlanner.Core.Application.DTO;
using iPlanner.Core.Application.DTO.Reports;
using iPlanner.Core.Application.Interfaces;
using iPlanner.Core.Application.Interfaces.Repository;
using iPlanner.Core.Entities.Reports;

namespace iPlanner.Core.Application.Services
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _reportsRepository;
        private readonly IMapper<ReportDTO, Report> _reportMapper;
        private readonly IMapper<ReportFilterDTO, ReportFilter> _filterMapper;

        public ReportService(
                IReportRepository repository, 
                IMapper<ReportDTO, Report> reportMapper,
                IMapper<ReportFilterDTO, ReportFilter> reportFilterMapper)
        {
            _reportsRepository = repository;
            _reportMapper = reportMapper;
            _filterMapper = reportFilterMapper;
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

        public async Task<List<ReportDTO>> GetReportsAsyncByFilter(ReportFilterDTO filterDTO)
        {
            ReportFilter filter = _filterMapper.ToEntity(filterDTO);
            if (filter.CheckTime())
            {
                List<ReportDTO> reportsDTO = await GetReportsAsync();
                List<Report> reports = reportsDTO.Select(r => _reportMapper.ToEntity(r)).ToList();
                List<Report> filteredReports = filter.FilterReports(reports);
                return filteredReports.Select(fr=>_reportMapper.ToDTO(fr)).ToList();
            }
            else
            {
                throw new Exception("Error, la fehc de inicio debe ser menor a la fecha fin");
            }

        }
    }
}

