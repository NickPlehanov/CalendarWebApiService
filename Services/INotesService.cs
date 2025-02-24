using CalendarWebApiService.Models;

namespace CalendarWebApiService.Services
{
    public interface INotesService
    {
        IEnumerable<Notes> GetAll();
        Notes GetById(int id);
        IEnumerable<Notes> GetByConditions(object cond);
        void Add(Notes entity);
        void Update(int id,Notes entity);
        bool Delete(int id);
    }
}
