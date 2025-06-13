using AutoMapper;
using Data.DTO.OrderDetails;
using Data.Enum;

namespace Data.DTO.Orders
{
    public class OrdersCreateUpdateRequest
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
        public List<OrderDetailsCreateUpdateRequest> orderDetails { get; set; }
        public class AutoMapperProfiles : Profile
        {
            public AutoMapperProfiles()
            {
                CreateMap<OrdersCreateUpdateRequest, Entities.Orders>();
            }
        }
    }
}
