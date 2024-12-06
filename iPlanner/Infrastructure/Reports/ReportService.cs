using iPlanner.Core.Application.Interfaces;
using iPlanner.Core.Entities.Reports;
using System.IO;
using System.Text.Json;

namespace iPlanner.Infrastructure.Reports
{
    public class ReportService : IReportService
    {
        public async Task<List<Report>> GetReportsAsync()
        {
            string jsonContent = await File.ReadAllTextAsync("C:\\Users\\usuario\\Downloads\\roc1-reports.json");
            return JsonSerializer.Deserialize<List<Report>>(jsonContent);
        }
    }
}


