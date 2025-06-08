namespace Data.Entities
{
    public class ExportDetails
    {
        public int  ExportDetailID { get; set; }
        public int ExportID { get; set; }
        public int LotID { get; set; }
        public float Quantity { get; set; }
        public ExportReceipts Export { get; set; }
        public Lot Lot { get; set; }
    }
}
