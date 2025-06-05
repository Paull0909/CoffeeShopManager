using AutoMapper;

namespace Data.DTO.Positions
{
    public class PositionsViewModel
    {
        public int PositionID { get; set; }
        public string PositionName { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid UserID { get; set; }
        public string UserName { get; set; }
        public class AutoMapperProfiles : Profile
        {
            public AutoMapperProfiles()
            {
                CreateMap<Entities.Positions, PositionsViewModel>();
            }
        }
    }
}
