﻿using Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace Data.Entities
{
    public class Orders
    {
        public int OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public int EmployeeID { get; set; }
        [Required]
        public string TableNumber { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal Discount { get; set; }
        public decimal FinalAmount { get; set; }
        public TransactionStatus PaymentStatus { get; set; }
        public List<OrderDetails> OrderDetails { get; set; }
        public Employees Employees { get; set; }
        public Payments Payments { get; set; }
    }
}
