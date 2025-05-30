﻿using AutoMapper;

namespace Data.DTO.ImportReceipts
{
    public class ImportReceiptsCreateUpdateRequest
    {
        public int ImportID { get; set; }
        public int SupplierID { get; set; }
        public DateTime ImportDate { get; set; }
        public string Note { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid UserID { get; set; }
        public class AutoMapperProfiles : Profile
        {
            public AutoMapperProfiles()
            {
                CreateMap<ImportReceiptsCreateUpdateRequest, Entities.ImportReceipts>();
            }
        }
    }
}
