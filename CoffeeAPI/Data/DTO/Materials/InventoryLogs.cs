using Data.DTO.LotDetails;

namespace Data.DTO.Materials
{
    public class InventoryLogs
    {
        public int MaterialID { get; set; }
        public string MaterialName { get; set; }
        public int LotID { get; set; }
        public int Quantity { get; set; }
        public int QuantityBefor { get; set; }
        public int QuantityAfter { get; set; }
        public string Status { get; set; }
        public DateTime CreateAt { get; set; }

    }
}
