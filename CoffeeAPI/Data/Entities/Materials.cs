using System.ComponentModel.DataAnnotations;

namespace Data.Entities
{
    public class Materials
    {
        public int MaterialID { get; set; }
        [Required]
        public string MaterialName { get; set; }
        [Required]
        public string Unit { get; set; }
        public int CategoryID { get; set; }
        public int SupplierID { get; set; }
        public int MinStock { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public float Quantity { get; set; }
        public decimal PurchasePrice { get; set; }
        public Guid UserID { get; set; }
        public User User { get; set; }
        public Categories_Material Categories_Material { get; set; }
        public Suppliers Suppliers { get; set; }
        public List<ExportDetails> ExportDetails { get; set; }
        public List<ImportDetails> ImportDetails { get; set; }
        public List<InventoryLogs> InventoryLogs { get; set; }
        public List<Recipes> Recipes { get; set; }
        public List<Lot> Lots { get; set; }
    }
}
