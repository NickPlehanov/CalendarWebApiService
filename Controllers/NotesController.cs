using CalendarWebApiService.Helpers;
using CalendarWebApiService.Models;
using CalendarWebApiService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;

namespace CalendarWebApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ODataController
    {
        private readonly INotesService _notesService;        
        private readonly AppSettings? _appSettings;
        public NotesController(INotesService notesService, IOptionsSnapshot<AppSettings> appSettings)
        {
            _notesService = notesService;
            _appSettings = appSettings.Value;
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
                return File(CsvHelper<Notes>.CreateCsv(list,_appSettings.Delimeter), _appSettings.CsvHeaderType, _appSettings.CsvFileName);
            }
        }
        [HttpPost]
        [EnableQuery]
        public async Task<IActionResult> Post([FromBody] Notes note)
        {
            if (note == null)
            {
                return BadRequest();
            }

            _notesService.Add(note);

            return Created(); 
        }
        [HttpPut("{key}")]
        [EnableQuery]
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
        [EnableQuery]
        public IActionResult Delete([FromODataUri] int key)
        {
            bool res = _notesService.Delete(key);

            return res ? NoContent() : NotFound(key);
        }
    }
}
