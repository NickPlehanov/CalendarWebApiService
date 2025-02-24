using CalendarWebApiService.Models;
using Microsoft.Extensions.Options;

namespace CalendarWebApiService.Services
{
    public class NotesService : INotesService
    {
        private readonly IRepository<Notes> _iNotesRepository;
        private readonly AppSettings? _appSettings;
        public NotesService(IRepository<Notes> repository, IOptionsSnapshot<AppSettings> appSettings)
        {
            _iNotesRepository = repository;
            _appSettings = appSettings.Value;
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

        public async IAsyncEnumerable<Notes> GetNotesReadyToAlarm()
        {
            var StartDate = DateTime.Now.AddSeconds(_appSettings.QueryPeriodForAlarm * (-1));
            var EndDate = DateTime.Now.AddSeconds(_appSettings.QueryPeriodForAlarm);
            foreach (var item in _iNotesRepository.GetAll().Where(x => x.Date >=StartDate && x.Date<=EndDate))
            {
                yield return item;
            }
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
