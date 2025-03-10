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
        public async Task Add(Notes entity)
        {
            await _iNotesRepository.Add(entity);
        }

        public async Task<bool> Delete(int id)
        {
            Notes nts = await _iNotesRepository.GetById(id);
            if (nts == null)
                return false;

            await _iNotesRepository.Delete(id);
            return true;
        }

        public async Task<IEnumerable<Notes>> GetAll()
        {
            return await _iNotesRepository.GetAll();
        }
        public async Task<Notes> GetById(int id)
        {
            return await _iNotesRepository.GetById(id);
        }

        public async IAsyncEnumerable<Notes> GetNotesReadyToAlarm()
        {
            var StartDate = DateTime.Now.AddSeconds(_appSettings.QueryPeriodForAlarm * (-1));
            var EndDate = DateTime.Now.AddSeconds(_appSettings.QueryPeriodForAlarm);
            var list = await _iNotesRepository.GetAll();
            foreach (var item in list)
            {
                if(item.Date>=StartDate && item.Date <= EndDate)
                yield return item;
            }
        }

        public async Task Update(int id,Notes entity)
        {
            Notes note = await _iNotesRepository.GetById(id);
            await _iNotesRepository.Update(id,entity);
        }
    }
}
