﻿using AutoMapper;
using Data.Enum;

namespace Data.DTO.Tables
{
    public class TablesCreateUpdateRequest
    {
        public int TableID { get; set; }
        public string TableName { get; set; }
        public TableStatus Status { get; set; }
        public int Capacity { get; set; }
        public int AreasID { get; set; }
        public class AutoMapperProfiles : Profile
        {
            public AutoMapperProfiles()
            {
                CreateMap<TablesCreateUpdateRequest, Entities.Tables>();
            }
        }
    }
}
