

using CalendarWebApiService.Data;
using Microsoft.EntityFrameworkCore;

namespace CalendarWebApiService.Services
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DataContext _context;
        public Repository(DataContext context)
        {
            _context = context;
        }
        public async Task Add(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            _context.Set<T>().Remove(await GetById(id));
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T?> GetById(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task Update(int id,T entity)
        {
            _context.Entry<T>(await GetById(id))?.CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
        }
    }
}
