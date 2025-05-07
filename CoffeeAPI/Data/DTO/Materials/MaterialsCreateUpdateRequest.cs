﻿using AutoMapper;

namespace Data.DTO.Materials
{
    public class MaterialsCreateUpdateRequest
    {
        public int MaterialID { get; set; }
        public string MaterialName { get; set; }
        public string Unit { get; set; }
        public int CategoryID { get; set; }
        public int MinStock { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public float Quantity { get; set; }
        public decimal PurchasePrice { get; set; }
        public Guid UserID { get; set; }
        public class AutoMapperProfiles : Profile
        {
            public AutoMapperProfiles()
            {
                CreateMap<MaterialsCreateUpdateRequest, Entities.Materials>();
            }
        }
    }
}
