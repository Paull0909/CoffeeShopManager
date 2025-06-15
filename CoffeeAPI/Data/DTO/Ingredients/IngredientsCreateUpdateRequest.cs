using AutoMapper;

namespace Data.DTO.Ingredients
{
    public class IngredientsCreateUpdateRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public class AutoMapperProfiles : Profile
        {
            public AutoMapperProfiles()
            {
                CreateMap<IngredientsCreateUpdateRequest, Entities.Ingredients>();
            }
        }
    }
}
