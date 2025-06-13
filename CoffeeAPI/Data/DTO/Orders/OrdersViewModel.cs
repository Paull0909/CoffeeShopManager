using AutoMapper;
using Data.Enum;

namespace Data.DTO.Orders
{
    public class OrdersViewModel
    {
        public int OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public int EmployeeID { get; set; }
        public int TableNumberID { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal Discount { get; set; }
        public decimal FinalAmount { get; set; }
        public bool OrderStatus { get; set; }
        public TransactionStatus PaymentStatus { get; set; }
        public class AutoMapperProfiles : Profile
        {
            public AutoMapperProfiles()
            {
                CreateMap<Entities.Orders, OrdersViewModel>();
            }
        }
    }
}
