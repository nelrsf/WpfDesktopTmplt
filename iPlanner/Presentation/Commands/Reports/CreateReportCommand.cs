using iPlanner.Core.Application.DTO;
using iPlanner.Core.Application.Interfaces;
using iPlanner.Presentation.Interfaces;
using iPlanner.Presentation.Services.AppMediator.Base;
using iPlanner.Presentation.Services.AppMediator.MediatorMessages;
using System.Windows;

namespace iPlanner.Presentation.Commands.Reports
{
    internal class CreateReportCommand : CommandInputMessageBase<ReportMessage>, ICommand
    {
        public event EventHandler? CanExecuteChanged;
        private readonly IReportService _reportService;

        public CreateReportCommand(IReportService reportService)
        {
            _reportService = reportService;
        }
        public bool CanExecute()
        {
            return true;
        }

        public void Execute()
        {
            if (!CanExecute()) return;
            if (message == null) return;
            ReportMessage reportMessage = message;
            if (reportMessage == null) return;
            ReportDTO? report = reportMessage.Report;
            if (report == null) return;
            try
            {
                _reportService.AddReportAsync(report);
                MessageBox.Show("Reporte agregado correctamente");
            }
            catch
            {
                MessageBox.Show("Error al agregar el reporte");
            }
        }
    }
}
