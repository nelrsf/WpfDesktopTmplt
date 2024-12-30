using iPlanner.Core.Application.DTO;
using iPlanner.Core.Application.Interfaces.Repository;
using iPlanner.Infrastructure.Common;

namespace iPlanner.Infrastructure.Reports
{
    public class FileReportsRepository : IReportRepository
    {
        private readonly FileService _fileService;
        private readonly string _reportsFilePath;

        public FileReportsRepository(FileService fileService)
        {
            _fileService = fileService;
            _reportsFilePath = _fileService.GetDataFilePath("Reports.json");
            _fileService.EnsureDirectoryExists(_reportsFilePath);
        }

        public async Task<List<ReportDTO>> GetReports()
        {
            try
            {
                return await Task.Run(() =>
                {
                    return _fileService.LoadJsonData<List<ReportDTO>>(_reportsFilePath)
                        ?? new List<ReportDTO>();
                });
            }
            catch (Exception ex)
            {
                throw new Exception("Error loading reports", ex);
            }
        }

        public async Task AddReport(ReportDTO report)
        {
            var reports = await GetReports();
            report.ReportId = IdGenerator.GenerateUUID();
            reports.Add(report);
            SaveReports(reports);
        }

        public async Task UpdateReport(ReportDTO updatedReport)
        {
            var reports = await GetReports();
            var index = reports.FindIndex(r => r.ReportId == updatedReport.ReportId);
            if (index != -1)
            {
                reports[index] = updatedReport;
                SaveReports(reports);
            }
        }

        public async Task DeleteReport(ReportDTO report)
        {
            var reports = await GetReports();
            reports.RemoveAll(r => r.ReportId == report.ReportId);
            SaveReports(reports);
        }

        private void SaveReports(List<ReportDTO> reports)
        {
            if (reports == null)
                throw new ArgumentNullException(nameof(reports));

            try
            {
                _fileService.SaveJsonData(_reportsFilePath, reports);
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving reports", ex);
            }
        }
    }
}
