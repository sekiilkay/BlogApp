
using BlogApp.Web.Context;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Web.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly BlogContext _context;
        private readonly DbSet<T> _dbSet;
        public GenericRepository(BlogContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return (await _dbSet.FindAsync(id))!;
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }
    }
}
