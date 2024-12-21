using iPlanner.Core.Application.Interfaces;
using iPlanner.Presentation.Services.MediatorMessages;
using System.Windows;

namespace iPlanner.Presentation.Commands.Reports
{
    internal class CreateReportCommand : ICommand<ReportMessage>
    {
        public event EventHandler? CanExecuteChanged;
        private readonly IReportService _reportService;

        public CreateReportCommand(IReportService reportService)
        {
            _reportService = reportService;
        }
        public bool CanExecute(ReportMessage? parameter)
        {
            return true;
        }

        public void Execute(ReportMessage? reportMessage)
        {
            if (reportMessage == null) return;
            _reportService.AddReportAsync(reportMessage.Report);
            MessageBox.Show("Reporte agragado correctamente");
        }
    }
}
