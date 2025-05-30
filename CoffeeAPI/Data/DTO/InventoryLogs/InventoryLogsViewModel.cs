﻿using AutoMapper;

namespace Data.DTO.InventoryLogs
{
    public class InventoryLogsViewModel
    {
        public int InventoryLogID { get; set; }
        public int MaterialID { get; set; }
        public DateTime ChangeDate { get; set; }
        public string ChangeType { get; set; }
        public float QuantityChange { get; set; }
        public string Note { get; set; }
        public class AutoMapperProfiles : Profile
        {
            public AutoMapperProfiles()
            {
                CreateMap<Entities.InventoryLogs, InventoryLogsViewModel>();
            }
        }
    }
}
