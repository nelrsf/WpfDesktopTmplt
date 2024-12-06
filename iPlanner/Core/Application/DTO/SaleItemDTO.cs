using iPlanner.Core.Entities.Dashboard;

namespace iPlanner.Core.Application.DTO
{
    public class SaleItemDTO
    {
        public string? Date { get; set; }
        public string? Customer { get; set; }
        public string? Product { get; set; }
        public string? Amount { get; set; }

        public SaleItemDTO(SaleItem saleItem)
        {
            Date = saleItem.Date;
            Customer = saleItem.Customer;
            Product = saleItem.Product;
            Amount = saleItem.Amount;
        }
    }
}
