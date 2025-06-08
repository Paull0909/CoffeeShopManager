using AutoMapper;

namespace Data.DTO.ProductSizes
{
    public class ProductSizesViewModel
    {
        public int ProductSizeID { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string SizeName { get; set; }
        public decimal AdditionalPrice { get; set; }
        public class AutoMapperProfiles : Profile
        {
            public AutoMapperProfiles()
            {
                CreateMap<Entities.ProductSizes, ProductSizesViewModel>();
            }
        }
    }
}
