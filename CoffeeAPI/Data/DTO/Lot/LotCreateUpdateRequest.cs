using AutoMapper;

namespace Data.DTO.Lot
{
   public class LotCreateUpdateRequest
    {
        public int LotID { get; set; }
        public string LotName { get; set; }
        public float Quantity { get; set; }
        public int MaterialID { get; set; }
        public decimal PurchasePrice { get; set; }
        public DateOnly ExpirationDate { get; set; }
        public string Status { get; set; }
        public class AutoMapperProfiles : Profile
        {
            public AutoMapperProfiles()
            {
                CreateMap<LotCreateUpdateRequest, Entities.Lot>();
            }
        }
    }
}
