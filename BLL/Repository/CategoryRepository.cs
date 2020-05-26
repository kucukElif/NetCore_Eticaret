using BLL.Abstract;
using DAL.Context;
using DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BLL.Repository
{
    public class CategoryRepository : ICategoryService
    {
        private readonly AppDbContext context;

        public CategoryRepository(AppDbContext context)
        {
            this.context = context;
        }
        public void Add(Category category)
        {
            context.Categories.Add(category);
            context.SaveChanges();
        }

        public bool Any(Expression<Func<Category, bool>> exp)
        {
            return context.Categories.Any(exp);
        }

        public List<Category> GetActive()
        {
            return context.Categories.Where(x => x.Status == DAL.Entity.Enum.Status.Active).ToList();
        }

        public Category GetById(Guid id)
        {
            return context.Categories.FirstOrDefault(x => x.ID == id);
        }

        public List<Category> GetDefault(Expression<Func<Category, bool>> exp)
        {
            return context.Categories.Where(exp).ToList();

        }

        public void Remove(Guid id)
        {
            Category category = GetById(id);
            category.Status = DAL.Entity.Enum.Status.Deleted;
            Update(category);
        }

        public void RemoveAll(Expression<Func<Category, bool>> exp)
        {
            foreach (var item in GetDefault(exp))
            {
                item.Status = DAL.Entity.Enum.Status.Deleted;
                Update(item);


            }
        }

        public void Update(Category category)
        {
            context.Entry(category).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
        }
    }
}
