using DAL.Entity.Base;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entity
{
    public class OrderDetail:CoreEntity
    {
        public Guid ProductID { get; set; }
        public virtual Product Product { get; set; }
        public virtual Order Order { get; set; }
        [Column(TypeName ="decimal(18,2)")]
        public decimal UnitPrice { get; set; }
        public short? Quantity { get; set; }
        public Guid OrderId { get; set; }
        public decimal GetTotalPrice()
        {
            return Convert.ToDecimal(UnitPrice * Quantity);
        }
    }
}