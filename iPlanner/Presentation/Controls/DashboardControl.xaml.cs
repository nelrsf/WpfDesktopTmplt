using iPlanner.Core.Application.Services;
using iPlanner.Presentation.ViewModels;
using System.Windows.Controls;

namespace iPlanner.Presentation.Controls
{
    public partial class DashboardControl : UserControl
    {
        public DashboardViewModel _dashboardViewModel;
        public DashboardControl()
        {
            InitializeComponent();
            _dashboardViewModel = new DashboardViewModel(new DashboardService());
            DataContext = _dashboardViewModel;
            InitializeSalesItems();
        }

        public void InitializeSalesItems()
        {
            _dashboardViewModel.InitializeGrid();
        }


    }
}