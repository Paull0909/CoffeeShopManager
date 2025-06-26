using AutoMapper;
using Data.DTO.Recipes;

namespace Data.DTO.Surcharges
{
    public class SurchargesViewModel
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
                CreateMap<Entities.Surcharges, SurchargesViewModel>();
            }
        }
    }
}
