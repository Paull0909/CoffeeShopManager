using AutoMapper;
using Data.DTO.Materials;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO.Lot
{
    public class LotViewModel
    {
        public int LotID { get; set; }
        public string LotName { get; set; }
        public float Quantity { get; set; }
        public int MaterialID { get; set; }
        public decimal PurchasePrice { get; set; }
        public DateOnly ExpirationDate { get; set; }
        public class AutoMapperProfiles : Profile
        {
            public AutoMapperProfiles()
            {
                CreateMap<Entities.Lot, LotViewModel>();
            }
        }
    }
}
