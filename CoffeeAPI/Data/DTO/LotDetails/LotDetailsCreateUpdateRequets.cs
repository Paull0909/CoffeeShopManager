using AutoMapper;

namespace Data.DTO.LotDetails
{
    public class LotDetailsCreateUpdateRequets
    {
        public int Id { get; set; }
        public int LotId { get; set; }
        public int Quantity { get; set; }
        public int QuantityBefor { get; set; }
        public int QuantityAfter { get; set; }
        public string Status { get; set; }
        public DateTime CreateAt { get; set; }
        public class AutoMapperProfiles : Profile
        {
            public AutoMapperProfiles()
            {
                CreateMap<LotDetailsCreateUpdateRequets, Entities.LotDetails>();
            }
        }
    }
}
