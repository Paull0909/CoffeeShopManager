using AutoMapper;
using Data.DTO.Payments;

namespace Data.DTO.Positions
{
    public class PositionsCreateUpdateRequest
    {
        public int PositionID { get; set; }
        public string PositionName { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid UserID { get; set; }
        public class AutoMapperProfiles : Profile
        {
            public AutoMapperProfiles()
            {
                CreateMap<PositionsCreateUpdateRequest, Entities.Positions>();
            }
        }
    }
}
