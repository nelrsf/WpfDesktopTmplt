using iPlanner.Core.Application.AppMediator.Base;
using iPlanner.Core.Application.Interfaces;
using iPlanner.Presentation.Services.MediatorMessages;
using iPlanner.Presentation.ViewModels.Reports;
using System.Windows.Forms;

namespace iPlanner.Presentation.Commands.Reports
{
    public class DeleteReportCommand : CommandInputMessageBase<ReportMessage>, ICommand
    {
        public event EventHandler? CanExecuteChanged;
        private IReportService _reportService;

        public DeleteReportCommand(IReportService reportService)
        {
            _reportService = reportService;
        }

        public bool CanExecute()
        {
            return true;
        }

        public async void Execute()
        {
            if (message == null) return;
            if (message.Report == null) return;
            var result = MessageBox.Show("Esta seguro que desea eliminar este reporte?", "Eliminar Reporte", MessageBoxButtons.YesNo);
            if (result == DialogResult.No) return;
            await _reportService.DeleteReportAsync(message.Report);
            if(message.sender is ReportListViewModel reportListViewModel)
            {
                await reportListViewModel.LoadReportsAsync();
            }
        }
    }
}