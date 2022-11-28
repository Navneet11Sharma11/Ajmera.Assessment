using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ajmera.Assessment.DL.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetAsync(Guid primaryKey);
        T Create(T entity);
        Task<T> CreateAsync(T entity);
        Task CreateAsync(List<T> entities);
        T Update(T entity);
        T Delete(T entity);
        void Delete(List<T> entities);
        IQueryable<T> GetDBSet(bool enableTracking = false);
    }
}
