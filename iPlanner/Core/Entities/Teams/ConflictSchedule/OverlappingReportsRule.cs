using iPlanner.Core.Entities.Reports;

namespace iPlanner.Core.Entities.Teams.ConflictSchedule
{
    public class OverlappingReportsRule : IConflictScheduleRule
    {
        public IEnumerable<ConflictItem> EvaluateConflicts(Report report, List<Report> reports)
        {
            var overlappingReports = reports.Where(r => r.Date == report.Date && r.ReportId != report.ReportId &&
                                                        r.Team.Id == report.Team.Id &&
                                                        ((r.TimeInit >= report.TimeInit && r.TimeInit < report.TimeEnd) ||
                                                         (r.TimeEnd > report.TimeInit && r.TimeEnd <= report.TimeEnd) ||
                                                         (r.TimeInit <= report.TimeInit && r.TimeEnd >= report.TimeEnd)));

            foreach (var overlap in overlappingReports)
            {
                yield return new ConflictItem
                {
                    TeamName = report.Team?.Name ?? "Desconocido",
                    TeamId = report.Team?.Id,
                    Date = report.Date.Value,
                    Description = $"Conflicto en los reportes del frente {report.Team.Name ?? "Desconocido"}, el dia {report.Date.Value}"
                };
            }
        }
    }
}
