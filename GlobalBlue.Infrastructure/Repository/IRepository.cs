using System.Linq;
using System.Threading.Tasks;

namespace GlobalBlue.Infrastructure.Repository
{
    public interface IRepository<T>
    {
        T Get(int id);

        Task<T> Create(T entity);

        Task Delete(int id);

        Task Delete(T entity);

        Task Update(T entity);

        IQueryable<T> Query();
    }
}