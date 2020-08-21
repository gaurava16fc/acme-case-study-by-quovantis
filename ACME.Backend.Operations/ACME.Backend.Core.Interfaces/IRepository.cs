using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ACME.Backend.Core.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task Create(T entity);
        IQueryable<T> FindAll();
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
        Task Update(T entity);
        Task Delete(T entity);
        Task<bool> SaveAll();
    }
}
