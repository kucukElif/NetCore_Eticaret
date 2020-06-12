using DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Areas.Admin.Models.ViewModels
{
    public class OrderVM
    {
        public List<Order> Orders { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }

    }
}
