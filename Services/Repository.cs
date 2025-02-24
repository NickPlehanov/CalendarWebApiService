

using CalendarWebApiService.Data;

namespace CalendarWebApiService.Services
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DataContext _context;
        public Repository(DataContext context)
        {
            _context = context;
        }
        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            _context.Set<T>().Remove(GetById(id));
            _context.SaveChanges();
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().AsEnumerable();
        }

        public IEnumerable<T> GetByConditions(object cond)
        {
            throw new NotImplementedException();
        }

        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public void Update(int id,T entity)
        {
            //_context.Set<T>().Entry(entity).P
            _context.Entry<T>(GetById(id)).CurrentValues.SetValues(entity);
            _context.SaveChanges();
        }
    }
}
