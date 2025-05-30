﻿using AutoMapper;

namespace Data.DTO.ExportDetails
{
    public class ExportDetailsViewModel
    {
        public int ExportDetailID { get; set; }
        public int ExportID { get; set; }
        public int MaterialID { get; set; }
        public float Quantity { get; set; }
        public class AutoMapperProfiles : Profile
        {
            public AutoMapperProfiles()
            {
                CreateMap<Entities.ExportDetails, ExportDetailsViewModel>();
            }
        }
    }
}
