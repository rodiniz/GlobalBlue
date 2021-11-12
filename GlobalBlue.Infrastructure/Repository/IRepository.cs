using System.Linq;
using System.Threading.Tasks;

namespace GlobalBlue.Infrastructure.Repository
{
    public interface IRepository<T>
    {
        T Get(int id);

        void Create(T entity);

        void Delete(int id);

        void Delete(T entity);

        void Update(T entity);
        Task SaveChanges();

        IQueryable<T> Query();
    }
}
