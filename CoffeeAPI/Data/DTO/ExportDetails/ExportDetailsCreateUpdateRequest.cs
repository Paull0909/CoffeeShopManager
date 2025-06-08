using AutoMapper;

namespace Data.DTO.ExportDetails
{
    public class ExportDetailsCreateUpdateRequest
    {
        public int ExportDetailID { get; set; }
        public int ExportID { get; set; }
        public int LotID { get; set; }
        public float Quantity { get; set; }
        
        public class AutoMapperProfiles : Profile
        {
            public AutoMapperProfiles()
            {
                CreateMap<ExportDetailsCreateUpdateRequest, Entities.ExportDetails>();
            }
        }
    }
}
