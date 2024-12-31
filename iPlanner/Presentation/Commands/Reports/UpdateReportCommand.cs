using iPlanner.Core.Application.Interfaces;
using iPlanner.Presentation.Interfaces;
using iPlanner.Presentation.Services.AppMediator.Base;
using iPlanner.Presentation.Services.AppMediator.MediatorMessages;
using System.Windows;

namespace iPlanner.Presentation.Commands.Reports
{
    public class UpdateReportCommand : CommandInputMessageBase<ReportMessage>, ICommand
    {
        public event EventHandler? CanExecuteChanged;
        private IReportService _reportService;

        public UpdateReportCommand(IReportService reportService)
        {
            _reportService = reportService;
        }

        public bool CanExecute()
        {
            return true;
        }

        public void Execute()
        {
            if(!CanExecute()) return;
            if(message == null) return;

            if (message.Report == null) return;

            try
            {
                _reportService.UpdateReportAsync(message.Report);

                MessageBox.Show("Reporte actualizado correctamente", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch
            {
                MessageBox.Show("Error al actualizar el reporte", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}