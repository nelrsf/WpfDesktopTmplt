using System.Collections.ObjectModel;
using iPlanner.Core.Application.DTO;
using iPlanner.Core.Entities.Dashboard;

namespace iPlanner.Core.Application.Services
{
    public class DashboardService
    {
        private ObservableCollection<SaleItem>? saleItems;

        public DashboardService()
        {
            LoadSampleData();
        }
        private void LoadSampleData()
        {
            saleItems = new ObservableCollection<SaleItem>
            {
                new SaleItem { Date = "17/11/2024", Customer = "Empresa ABC", Product = "Servicio Premium", Amount = "$1,299.00" },
                new SaleItem { Date = "17/11/2024", Customer = "Juan Pérez", Product = "Licencia Anual", Amount = "$599.00" },
                new SaleItem { Date = "16/11/2024", Customer = "María García", Product = "Servicio Básico", Amount = "$299.00" },
                new SaleItem { Date = "16/11/2024", Customer = "Empresa XYZ", Product = "Servicio Enterprise", Amount = "$2,499.00" },
                new SaleItem { Date = "15/11/2024", Customer = "Carlos López", Product = "Licencia Mensual", Amount = "$49.00" }
            };
        }

        public ObservableCollection<SaleItemDTO> GetSaleItems()
        {
            return new ObservableCollection<SaleItemDTO>(
                    saleItems.Select(item => new SaleItemDTO(item))
            );
        }
    }
}
