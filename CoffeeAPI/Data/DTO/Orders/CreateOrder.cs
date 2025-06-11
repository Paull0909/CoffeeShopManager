using Data.DTO.OrderDetails;
using Data.DTO.OrderToppingDetails;
using System.Transactions;

namespace Data.DTO.Orders
{
    public class CreateOrder
    {
        public DateTime OrderDate { get; set; }
        public int EmployeeID { get; set; }
        public int TableNumberID { get; set; }
        public decimal Discount { get; set; }
        public int PaymentStatus { get; set; }

        public List<CreateOrderDetails> orderDetails { get; set; }
    }
}
