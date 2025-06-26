using AutoMapper;

namespace Data.DTO.Surcharges
{
    public class SurchargesCreateUpdateRequest
    {
        public int ID { get; set; }
        public string SurchargesName { get; set; }
        public int SurchargesValue { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public class AutoMapperProfiles : Profile
        {
            public AutoMapperProfiles()
            {
                CreateMap<SurchargesCreateUpdateRequest, Entities.Surcharges>();
            }
        }
    }
}
