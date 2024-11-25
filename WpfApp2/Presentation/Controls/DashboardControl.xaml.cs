using System.Collections.ObjectModel;
using System.Windows.Controls;
using WpfDesktopTmplt.Core.Application.Services;
using WpfDesktopTmplt.Core.Entities.Dashboard;
using WpfDesktopTmplt.Presentation.ViewModels;

namespace WpfDesktopTmplt.Presentation.Controls
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