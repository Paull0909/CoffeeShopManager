using System.ComponentModel.DataAnnotations;

namespace Data.Entities
{
    public class Suppliers
    {
        public int SupplierID { get; set; }
        [Required]
        public string SupplierName { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<ImportReceipts> ImportReceipts { get; set; }
        public List<Materials> Materials { get; set; }
    }
}
