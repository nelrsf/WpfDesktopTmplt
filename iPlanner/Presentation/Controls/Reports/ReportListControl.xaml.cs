using iPlanner.Core.Application.DTO;
using iPlanner.Core.Application.Interfaces;
using iPlanner.Presentation.Interfaces;
using iPlanner.Presentation.ViewModels.Reports;
using System.Windows;
using System.Windows.Controls;

namespace iPlanner.Presentation.Controls
{
    public partial class ReportListControl : UserControl
    {
        public ReportListViewModel ReportListViewModel { get; set; }
        public ReportListControl()
        {
            InitializeComponent();
            IReportService reportService = AppServices.GetService<IReportService>();
            IControlAbstractFactory abstractFactory = AppServices.GetService<IControlAbstractFactory>();
            ReportListViewModel = AppServices.GetService<ReportListViewModel>();
            DataContext = ReportListViewModel;
            Loaded += ReportListControl_Loaded;
        }

        private async void ReportListControl_Loaded(object sender, RoutedEventArgs e)
        {
            await ReportListViewModel.LoadReportsAsync();
        }

        public void OnCreateReport(object sender, EventArgs e)
        {
            ReportListViewModel.OpenCreateReportForm();
        }

        public void OnDeleteReport(object sender, EventArgs e)
        {
            if (sender is Button button && button.DataContext is ReportDTO report)
            {
                ReportListViewModel.DeleteReport(report);
            }
        }

        public async void OnRefreshList(object sender, EventArgs e)
        {
            await ReportListViewModel.LoadReportsAsync();
        }

        public void OnReadReport(object sender, EventArgs e)
        {
            SetReportAction(sender, ReportListViewModel.ViewDetails);
        }

        public void OnEditReport(object sender, EventArgs e)
        {
            SetReportAction(sender, ReportListViewModel.EditReport);
        }

        private void SetReportAction(object sender, Action<ReportDTO> action)
        {
            if (sender is Button button && button.DataContext is ReportDTO report)
            {
                if (action != null)
                {
                    action.Invoke(report);
                }
            }
        }

    }

}
