using Data.DTO.Products;

namespace Data.DTO.Report
{
    public class ReportViewModel
    {
        public decimal TotalRevenue { get; set; }
        public int Number_Employee { get; set; }
        public int Number_Orders { get; set; }
        public List<ProductReport> Products { get; set; }
    }
}
