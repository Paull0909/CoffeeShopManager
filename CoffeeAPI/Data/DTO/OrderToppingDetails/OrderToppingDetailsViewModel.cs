using AutoMapper;

namespace Data.DTO.OrderToppingDetails
{
    public class OrderToppingDetailsViewModel
    {
        public int OrderToppingDetailID { get; set; }
        public int ToppingID { get; set; }
        public int OrderDetailID { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public class AutoMapperProfiles : Profile
        {
            public AutoMapperProfiles()
            {
                CreateMap< Entities.OrderToppingDetails, OrderToppingDetailsViewModel>();
            }
        }
    }
}
