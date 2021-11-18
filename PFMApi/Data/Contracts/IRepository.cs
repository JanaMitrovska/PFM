using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PFMApi.Data.Contracts
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetById(int id);     
        Task<ICollection<T>> List();
        Task AddRange(ICollection<T> collection);
        Task Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        IQueryable<T> AsQueryable();
        Task<bool> SaveAll();
    }
}
