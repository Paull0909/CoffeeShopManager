﻿using AutoMapper;

namespace Data.DTO.Products
{
    public class ProductsCreateUpdateRequest
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int CategoryID { get; set; }
        public decimal Price { get; set; }
        public decimal CostPrice { get; set; }
        public string Description { get; set; }
        public bool IsAvailable { get; set; }
        public class AutoMapperProfiles : Profile
        {
            public AutoMapperProfiles()
            {
                CreateMap<ProductsCreateUpdateRequest, Entities.Products>();
            }
        }
    }
}
