using Data.DTO.ImportDetails;

namespace Data.DTO.ImportReceipts
{
    public class ImportReceiptsDTO
    {
        public int ImportID { get; set; }
        public int SupplierID { get; set; }
        public DateTime ImportDate { get; set; }
        public string Note { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid UserID { get; set; }
        public List<ImportDetailsDTO> ImportDetails { get; set; }
    }
}
