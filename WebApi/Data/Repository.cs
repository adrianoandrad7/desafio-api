using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Data
{
    public class Repository<TEntity> where TEntity : class
    {
        private readonly ApiContext _context;

        public Repository(ApiContext context)
        {
            _context = context;
        }

        public virtual void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }
        public virtual async Task AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
        }
        public virtual void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }
        public virtual async Task DeleteAsync(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;
            await Task.FromResult(_context.Set<TEntity>().Remove(entity));
        }
        public virtual void Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.Set<TEntity>().Update(entity);
        }
        public async Task UpdateAsync(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await Task.FromResult(_context.Set<TEntity>().Update(entity));
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
