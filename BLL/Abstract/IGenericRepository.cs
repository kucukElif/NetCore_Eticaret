using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BLL.Abstract
{
    public interface IGenericRepository<T> where T : class
    {
        List<T> GetActive();
        List<T> GetDefault(Expression<Func<T, bool>> exp);
        void Add(T entity);
        void Update(T entity);
        void Remove(Guid id);
        T GetById(Guid id);
        void RemoveAll(Expression<Func<T, bool>> exp);
        bool Any(Expression<Func<T, bool>> exp);
    }
}
