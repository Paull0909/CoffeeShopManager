using Data.DTO.OrderToppingDetails;

namespace Data.DTO.OrderDetails
{
    public class CreateOrderDetails
    {
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public int SizeID { get; set; }
        public int Quantity { get; set; }
        public List<CreateOrderToppingDetail> OrderToppingDetails { get; set; }
    }
}
