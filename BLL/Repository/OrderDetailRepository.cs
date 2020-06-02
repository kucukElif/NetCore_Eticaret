using BLL.Abstract;
using DAL.Context;
using DAL.Entity;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BLL.Repository
{
    public class OrderDetailRepository : IOrderDetailService
    {
        private readonly AppDbContext context;

        public OrderDetailRepository(AppDbContext context)
        {
            this.context = context;
        }
        public void Add(OrderDetail orderDetail)
        {
            context.OrderDetails.Add(orderDetail);
            context.SaveChanges();
        }

        public bool Any(Expression<Func<OrderDetail, bool>> exp)
        {
            return context.OrderDetails.Any(exp);
        }

        public List<OrderDetail> GetActive()
        {
            return context.OrderDetails.Where(x => x.Status == DAL.Entity.Enum.Status.Active).ToList();

        }

        public OrderDetail GetById(Guid id)
        {
            return context.OrderDetails.FirstOrDefault(x => x.ID == id);

        }

        public List<OrderDetail> GetDefault(Expression<Func<OrderDetail, bool>> exp)
        {
            return context.OrderDetails.Where(exp).ToList();
        }

        public void Remove(Guid id)
        {
            OrderDetail orderDetail = GetById(id);
            orderDetail.Status = DAL.Entity.Enum.Status.Deleted;
            Update(orderDetail);
        }

        public void RemoveAll(Expression<Func<OrderDetail, bool>> exp)
        {
            foreach (var item in GetDefault(exp))
            {
                item.Status = DAL.Entity.Enum.Status.Deleted;
                Update(item);
            }
        }

        public void Update(OrderDetail orderDetail)
        {
            context.Entry(orderDetail).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
                
        }
    }
}
