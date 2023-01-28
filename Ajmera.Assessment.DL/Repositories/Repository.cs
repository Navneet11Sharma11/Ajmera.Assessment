using Ajmera.Assessment.DL.Models;
using Microsoft.EntityFrameworkCore;

namespace Ajmera.Assessment.DL.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected AjmeraContext context;

        public Repository(AjmeraContext context)
        {
            this.context = context;
        }

        public T Create(T entity)
        {
            return context.Add(entity).Entity;
        }

        public async Task<T> CreateAsync(T entity)
        {
            var result = await context.AddAsync(entity);
            return result.Entity;
        }

        public Task CreateAsync(List<T> entities)
        {
            return context.AddRangeAsync(entities);
        }

        public void Delete(List<T> entities)
        {
            context.RemoveRange(entities);
        }

        public T Delete(T entity)
        {
            return context.Remove(entity).Entity;
        }

        public async Task<T> GetAsync(Guid primaryKey)
        {
            var entity = await context.FindAsync(entityType: typeof(T), keyValues: primaryKey);
            return (T)entity;
        }

        public IQueryable<T> GetDBSet(bool enableTracking = false)
        {
            if (enableTracking)
            {
                return context.Set<T>();
            }
            else
            {
                return context.Set<T>().AsNoTracking();
            }
        }

        public T Update(T entity)
        {
            return context.Update(entity).Entity;
        }


    }
}
