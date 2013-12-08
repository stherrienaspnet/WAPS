using System.Collections.Generic;

namespace WAPS.BookStore.Domain.Repositories.Abstract
{
    public interface IGenericRepository<T> where T : class
    {
        T GetById(object id);
        IEnumerable<T> GetAll();
        void Create(T entity);
        void Update(T entity);
        void Delete(object id);
    }
}
