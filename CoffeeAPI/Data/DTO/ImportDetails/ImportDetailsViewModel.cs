using AutoMapper;

namespace Data.DTO.ImportDetails
{
    public class ImportDetailsViewModel
    {
        public int ImportDetailID { get; set; }
        public int ImportID { get; set; }
        public int MaterialID { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
        public DateOnly ExpirationDate { get; set; }
        public string MaterialsName { get; set; }
        public class AutoMapperProfiles : Profile
        {
            public AutoMapperProfiles()
            {
                CreateMap<Entities.ImportDetails, ImportDetailsViewModel>();
            }
        }
    }
}
