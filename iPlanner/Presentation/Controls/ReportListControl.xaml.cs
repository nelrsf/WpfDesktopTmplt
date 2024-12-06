

using iPlanner.Infrastructure.Reports;
using iPlanner.Presentation.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace iPlanner.Presentation.Controls
{
    /// <summary>
    /// Lógica de interacción para ReportListControl.xaml
    /// </summary>

    public partial class ReportListControl : UserControl
    {
        public ReportListControl()
        {
            InitializeComponent();
            DataContext = new ReportListViewModel(new ReportService());
            Loaded += ReportListControl_Loaded;
        }

        private async void ReportListControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is ReportListViewModel viewModel)
            {
                await viewModel.LoadReportsAsync();
            }
        }
    }

}
