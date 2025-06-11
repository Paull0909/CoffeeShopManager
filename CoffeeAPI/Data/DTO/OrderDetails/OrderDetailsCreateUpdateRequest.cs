using AutoMapper;
using Data.DTO.OrderToppingDetails;

namespace Data.DTO.OrderDetails
{
    public class OrderDetailsCreateUpdateRequest
    {
        public int OrderDetailID { get; set; }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public int SizeID { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public List<OrderToppingDetailsCreateUpdateReuqest> OrderToppingDetails { get; set; }

        public class AutoMapperProfiles : Profile
        {
            public AutoMapperProfiles()
            {
                CreateMap<OrderDetailsCreateUpdateRequest, Entities.OrderDetails>();
            }
        }
    }
}
