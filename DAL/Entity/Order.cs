using DAL.Entity.Base;
using System;
using System.Collections.Generic;

namespace DAL.Entity
{
    public class Order:CoreEntity
    {
        public Order()
        {
            this.Confirmed = false;
            OrderDetails = new List<OrderDetail>();
        }
        public Guid AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public bool Confirmed { get; set; }
        public virtual List<OrderDetail> OrderDetails { get; set; }
    }
}