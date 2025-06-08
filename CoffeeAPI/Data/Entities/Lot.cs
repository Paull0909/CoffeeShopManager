using System.ComponentModel.DataAnnotations;

namespace Data.Entities
{
    public class Lot
    {
        public int LotID { get; set; }
        [Required]
        public string LotName { get; set; }
        public float Quantity { get; set; }
        public int MaterialID { get; set; }
        public decimal PurchasePrice { get; set; }
        public DateOnly ExpirationDate { get; set; }
        public Materials Materials { get; set; }
        public List<ExportDetails> ExportDetails { get; set; }
        public List<LotDetails> LotDetails { get; set; }
    }
}
