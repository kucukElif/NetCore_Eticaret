using BLL.Abstract;
using DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BLL.Repository
{
   public interface IOrderService :IGenericRepository<Order>
    {
    }
}
