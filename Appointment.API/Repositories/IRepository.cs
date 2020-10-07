using System.Linq;
using System.Threading.Tasks;

namespace Appointment.API.Repositories
{
    public interface IRepository<TEntity, TKey> where TEntity : class, new()
    {
        IQueryable<TEntity> GetAll();
        
        Task<TEntity> AddAsync(TEntity entity);

        Task<TEntity> UpdateAsync(TEntity entity);
        
        Task<TEntity> DeleteAsync(TEntity entity);
    }
}