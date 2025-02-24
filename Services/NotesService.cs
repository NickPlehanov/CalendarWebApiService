using CalendarWebApiService.Models;

namespace CalendarWebApiService.Services
{
    public class NotesService : INotesService
    {
        private readonly IRepository<Notes> _iNotesRepository;
        public NotesService(IRepository<Notes> repository)
        {
            _iNotesRepository = repository;
        }
        public void Add(Notes entity)
        {
            _iNotesRepository.Add(entity);
        }

        public bool Delete(int id)
        {
            Notes nts = _iNotesRepository.GetById(id);
            if (nts == null)
                return false;

            _iNotesRepository.Delete(id);
            return true;
        }

        public IEnumerable<Notes> GetAll()
        {
            return _iNotesRepository.GetAll();
        }

        public IEnumerable<Notes> GetByConditions(object cond)
        {
            throw new NotImplementedException();
        }

        public Notes GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(int id,Notes entity)
        {
            Notes note = _iNotesRepository.GetById(id);
            _iNotesRepository.Update(id,entity);
        }
    }
}
