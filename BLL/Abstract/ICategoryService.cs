using DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BLL.Abstract
{
    public interface ICategoryService
    {
        List<Category> GetActive();
        List<Category> GetDefault(Expression<Func<Category, bool>> exp);
        void Add(Category category);
        void Update(Category category);
        void Remove(Guid id);
        Category GetById(Guid id);
        void RemoveAll(Expression<Func<Category, bool>> exp);
        bool Any(Expression<Func<Category, bool>>exp); 
    }
}
