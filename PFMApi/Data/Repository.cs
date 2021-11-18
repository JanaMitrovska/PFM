
using Microsoft.EntityFrameworkCore;
using PFMApi.Data.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PFMApi.Data
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _dbContex;
        private readonly DbSet<T> entities;

        public Repository(AppDbContext dbContex)
        {
            _dbContex = dbContex;
            entities = _dbContex.Set<T>();
        }

        public async Task Add(T entity)
        {
            await entities.AddAsync(entity);
        }

        public async Task AddRange(ICollection<T> collection)
        {
            await entities.AddRangeAsync(collection);
        }

        public IQueryable<T> AsQueryable()
        {
            return entities.AsQueryable<T>();
        }

        public void Delete(T entity)
        {
            entities.Remove(entity);
        }

        public async Task<T> GetById(int id)
        {
            return await entities.FindAsync(id);
        }

        public async Task<ICollection<T>> List()
        {
            return await entities.ToListAsync();
        }

        public void Update(T entity)
        {
            entities.Attach(entity).State = EntityState.Modified;
        }

        public async Task<bool> SaveAll()
        {

            return await _dbContex.SaveChangesAsync() > 0;
        }

    }
}
