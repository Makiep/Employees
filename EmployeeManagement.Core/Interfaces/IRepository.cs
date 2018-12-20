using System.Collections.Generic;
using System.Linq;

namespace EmployeeManagement.Core.Interfaces
{
    public interface IRepository<T>
    {
        IQueryable<T> GetAll();
        T Get(long id);
        void Insert(T entity);
        T InsertWithReturn(T entity);
        void Update(T entity);
        void Delete(T entity);
        void AddRange(IEnumerable<T> entities);
        void RemoveRange(IEnumerable<T> entities);
    }
}