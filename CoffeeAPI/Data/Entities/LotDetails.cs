namespace Data.Entities
{
    public  class LotDetails
    {
        public int Id { get; set; }
        public int LotId { get; set; }
        public int Quantity { get; set; }
        public int QuantityBefor { get; set; }
        public int QuantityAfter { get; set; }
        public string Status { get; set; }
        public DateTime CreateAt { get; set; }
        public Lot Lot { get; set; }
    }
}
