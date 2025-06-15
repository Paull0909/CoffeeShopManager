using AutoMapper;
using Data.DTO.ImportReceipts;

namespace Data.DTO.Ingredients
{
    public class IngredientsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public class AutoMapperProfiles : Profile
        {
            public AutoMapperProfiles()
            {
                CreateMap<Entities.Ingredients, IngredientsViewModel>();
            }
        }
    }
}
