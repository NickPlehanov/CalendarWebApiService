﻿using CalendarWebApiService.Models;

namespace CalendarWebApiService.Services
{
    public interface INotesService
    {
        Task<Notes> GetById(int id);
        Task<IEnumerable<Notes>> GetAll();
        IAsyncEnumerable<Notes> GetNotesReadyToAlarm();
        Task Add(Notes entity);
        Task Update(int id,Notes entity);
        Task<bool> Delete(int id);
    }
}
