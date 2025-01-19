using iPlanner.Presentation.Controls;
using iPlanner.Presentation.Controls.Layout.RibbonTools;
using iPlanner.Presentation.Interfaces;
using iPlanner.Presentation.Services.AppMediator.Base;
using iPlanner.Presentation.Services.AppMediator.MediatorMessages;
using iPlanner.Presentation.ViewModels.Reports;

namespace iPlanner.Presentation.Commands.Reports
{
    public class SyncReportFilterCommand : CommandInputMessageBase<SyncReportFilterMessage>, ICommand
    {
        public event EventHandler? CanExecuteChanged;
        private bool _canExecute;



        public bool CanExecute()
        {
            return _canExecute;
        }

        public void Execute()
        {
            if (message == null || message.window == null) return;

            ReportsFilterViewmodel reportsFilterViewmodel = GetReportsFilterTools(message.window);
            _canExecute = message.Control is ReportListControl;
            reportsFilterViewmodel.CanExecute = _canExecute;
            reportsFilterViewmodel.ReportFilterDTO = null;
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);

            if (!_canExecute) return;

            if (message.Control is not ReportListControl reportListControl) return;

            ReportListViewModel reportsListViewModel = reportListControl.ReportListViewModel;
            reportsListViewModel.ReportFilterManager.InitializeFilters();
            reportsFilterViewmodel.ReportListViewModel = null;
            reportsFilterViewmodel.ReportListViewModel = reportsListViewModel;
        }

        private ReportsFilterViewmodel GetReportsFilterTools(MainWindow mainWindow)
        {
            ReportsFilterViewmodel ribbonVm = mainWindow.Ribbon.ReportsFilterToolbar.ReportsFilterViewmodel;
            return ribbonVm;
        }
    }
}
