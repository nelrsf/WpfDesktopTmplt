using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iPlanner.Core.Application.DTO;
using iPlanner.Core.Application.Services;

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
