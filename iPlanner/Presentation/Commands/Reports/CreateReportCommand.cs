using iPlanner.Core.Application.Interfaces;
using iPlanner.Presentation.Services.MediatorMessages;
using System.Windows;

namespace iPlanner.Presentation.Commands.Reports
{
    internal class CreateReportCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        private readonly IReportService _reportService;

        public CreateReportCommand(IReportService reportService) { 
            _reportService = reportService;
        }
        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            if (parameter is ReportMessage reportMessage) {
                _reportService.AddReportAsync(reportMessage.Report);
                MessageBox.Show("Reporte agragado correctamente");
            }
        }
    }
}
