using GlobalBlue.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace GlobalBlue.Infrastructure.Repository
{
    public abstract class BaseRepository<T> : IRepository<T> where T : class
    {
        protected readonly DataContext _db;

        protected BaseRepository(DataContext ctx)
        {
            _db = ctx;
        }

        public async Task<T> Create(T entity)
        {
            _db.Set<T>().Add(entity);
            await _db.SaveChangesAsync();
            _db.Entry(entity).Reload();
            return entity;
        }

        public async Task Delete(int id)
        {
            var entity = _db.Set<T>().Find(id);
            _db.Set<T>().Remove(entity);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(T entity)
        {
            _db.Set<T>().Remove(entity);
            await _db.SaveChangesAsync();
        }

        public T Get(int id)
        {
            return _db.Set<T>().Find(id);
        }
        public IQueryable<T> Query()
        {
            return _db.Set<T>().AsNoTracking();
        }       
        public async Task Update(T entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }
    }
}
