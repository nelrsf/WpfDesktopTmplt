using iPlanner.Core.Application.DTO;
using iPlanner.Core.Application.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace iPlanner.Presentation.ViewModels
{
    public class DashboardViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<SaleItemDTO>? salesGrid { get; set; }

        private DashboardService _dashboardService;

        public DashboardViewModel(DashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        public void InitializeGrid()
        {
            salesGrid = _dashboardService.GetSaleItems();
        }
    }
}
