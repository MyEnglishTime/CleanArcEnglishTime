using Microsoft.EntityFrameworkCore;
using WebApi.Domain.Abstractions;

namespace WebApi.Database.Common;

   public abstract class BaseRepository<T> : IDisposable, IBaseRepository<T> where T : class
    {
        private readonly EnglishTimeContext _db;
        private readonly DbSet<T> _dbSet;
        private bool _disposed = false;

        public BaseRepository(EnglishTimeContext dbContext)
        {
            _db = dbContext;
            _dbSet = _db.Set<T>();
        }

        public virtual async Task<IEnumerable<T>> FetchAllAsync() => await _dbSet.AsNoTracking().ToListAsync();

        public ValueTask<T?> FetchByIdAsync(int id) => _dbSet.FindAsync(id);

        public async Task CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task CreateRangeAsync(IEnumerable<T> items) => await _dbSet.AddRangeAsync(items);

        public Task UpdateAsync(T entity)
        {
            _db.Attach(entity);
            _db.Entry(entity).State = EntityState.Modified;
            return Task.CompletedTask;
        }

        public Task UpdateRangeAsync(IEnumerable<T> items)
        {
            _db.UpdateRange(items);
            return Task.CompletedTask;
        }

        public async Task DeleteAsync(int id)
        {
            T entity = await _dbSet.FindAsync(id);
            if (entity != null)
                _dbSet.Remove(entity);
        }

        public Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            return Task.CompletedTask;
        }

        public Task DeleteRangeAsync(IEnumerable<T> items)
        {
            _dbSet.RemoveRange(items);
            return Task.CompletedTask;
        }

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                _disposed = true;
                if (disposing)
                {
                    _db.Dispose();
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }