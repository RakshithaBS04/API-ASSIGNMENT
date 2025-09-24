using Microsoft.EntityFrameworkCore;
using SMARTLIBRARY.Data;
using SMARTLIBRARY.Interfaces.Repositories;
using System.Linq.Expressions;

namespace SMARTLIBRARY.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly SmartLibraryDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(SmartLibraryDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();
        public async Task<T?> GetByIdAsync(object id) => await _dbSet.FindAsync(id);
        public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);
        public void Update(T entity) => _dbSet.Update(entity);
        public void Delete(T entity) => _dbSet.Remove(entity);
        public async Task<bool> ExistsAsync(System.Linq.Expressions.Expression<Func<T, bool>> predicate) => await _dbSet.AnyAsync(predicate);
        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}
