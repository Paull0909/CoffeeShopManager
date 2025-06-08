using AutoMapper;

namespace Data.DTO.ImportDetails
{
    public class ImportDetailsCreateUpdateRequest
    {
        public int ImportDetailID { get; set; }
        public int ImportID { get; set; }
        public int MaterialID { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
        public DateOnly ExpirationDate { get; set; }
        public class AutoMapperProfiles : Profile
        {
            public AutoMapperProfiles()
            {
                CreateMap<ImportDetailsCreateUpdateRequest, Entities.ImportDetails>();
            }
        }
    }
}
