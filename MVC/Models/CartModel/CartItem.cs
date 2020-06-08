using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Models.CartModel
{
    public class CartItem
    {
        public CartItem()
        {
            Quantity = 1;
        }
        public Guid ID { get; set; }
        public string Name { get; set; }
        public short Quantity { get; set; }
        public decimal? Price { get; set; }
        public string ImagePath { get; set; }
        public decimal? SubTotal { get { return Price * Quantity; } }
    }
}
