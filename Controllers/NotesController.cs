using CalendarWebApiService.Helpers;
using CalendarWebApiService.Models;
using CalendarWebApiService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace CalendarWebApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ODataController
    {
        private readonly INotesService _notesService;
        //private readonly IHubContext<NotificationHub> _hubContext;
        public NotesController(INotesService notesService/*, IHubContext<NotificationHub> hubContext*/)
        {
            _notesService = notesService;
            //_hubContext = hubContext;
        }
        /// <summary>
        /// Получение списка всех заметок
        /// </summary>
        /// <returns>Список заметок</returns>
        [HttpGet]
        [EnableQuery]
        public ActionResult<IEnumerable<Notes>> GetListNotes([FromODataUri] bool export = false)
        {
            var list = _notesService.GetAll();
            if (!export) {
                return Ok(list);
            }
            else
            {
                //TODO: вынести в хелпер
                var csv = new StringBuilder();
                csv.AppendLine("Id,Title,Text,Date,CreateDate"); // Заголовки
                foreach (var note in list)
                {
                    csv.AppendLine($"{note.Id},{note.Title},{note.Text},{note.Date},{note.CreateDate}");
                }
                return File(new UTF8Encoding().GetBytes(csv.ToString()), "text/csv", "notes.csv");
            }
            //try
            //{
            //    return Ok(_notesService.GetAll());
            //}
            //catch (Exception e)
            //{
            //    return BadRequest(e.Message);
            //}
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Notes note)
        {
            if (note == null)
            {
                return BadRequest();
            }

            _notesService.Add(note);
            //await _hubContext.Clients.All.SendAsync("ReceiveNotification", $"Создана новая заметка: {note.Title}");
            //NotificationHub notificationHub = new();
            //await notificationHub.SendNotification($"Создана новая заметка: {note.Title}");

            return Created(); 
        }
        [HttpPut("{key}")]
        public IActionResult Put([FromODataUri] int key, [FromBody] Notes note)
        {
            //ModelState.IsValid
            if (note == null)
            {
                return BadRequest();
            }

            _notesService.Update(key, note);

            return NoContent();
        }
        //[HttpPatch("{key}")]
        //public IActionResult Put([FromODataUri] Guid noteId, [FromBody] Microsoft.AspNetCore.OData.Deltas.Delta<Notes> delta_note)
        //{
        //    //ModelState.IsValid
        //    //if (note == null)
        //    //{
        //    //    return BadRequest();
        //    //}

        //    //_notesService.Update(note);
        //    delta_note.Patch(_notesService.GetById(noteId));
        //    //_notesService.Update(noteId, delta_note.);

        //    return NoContent();
        //}
        [HttpDelete("{key}")]
        public IActionResult Delete([FromODataUri] int key)
        {
            bool res = _notesService.Delete(key);

            return res ? NoContent() : NotFound(key);
        }
    }
}
