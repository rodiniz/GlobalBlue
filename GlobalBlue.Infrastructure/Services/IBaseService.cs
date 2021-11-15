using System.Collections.Generic;
using System.Threading.Tasks;

namespace GlobalBlue.Infrastructure.Services
{
    public interface IBaseService<T>
    {
        T Get(int id);

        Task<T> Create(T entity);

        Task Update(T entity);

        Task Delete(int idEntity);

        List<T> GetAll();
    }
}