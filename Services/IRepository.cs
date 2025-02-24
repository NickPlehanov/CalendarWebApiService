namespace CalendarWebApiService.Services
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        IEnumerable<T> GetByConditions(object cond);
        void Add(T entity);
        void Update(int id,T entity);
        void Delete(int id);
    }
}
