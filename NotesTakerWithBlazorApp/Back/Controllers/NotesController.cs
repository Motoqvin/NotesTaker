using Microsoft.AspNetCore.Mvc;
using Back.Models;
using Back.Services.Base;

namespace Back.Controllers
{
    public class NotesController : ControllerBase
    {
        private readonly INoteService noteService;

        public NotesController(INoteService noteService)
        {
            this.noteService = noteService;
        }

        [HttpGet]
        public IActionResult Index(string? search)
        {
            var notes = noteService.GetAllNotesAsync();

            if (!string.IsNullOrWhiteSpace(search))
            {
                notes = notes
                    .Where(n => n.Title != null && n.Title.Contains(search, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            return Ok(notes);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var note = await noteService.GetNoteByIdAsync(id);
            if (note == null) return NotFound();
            return Ok(note);
        }

        [HttpPost]
        public async Task<IActionResult> Create(string Title)
        {
            if (string.IsNullOrWhiteSpace(Title))
                return RedirectToAction(nameof(Index));

            var newNote = new Note
            {
                Title = Title
            };

            await noteService.PostNoteAsync(newNote);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, string content)
        {
            var note = await noteService.GetNoteByIdAsync(id);
            if (note == null) return NotFound();

            note.Content = content;
            await noteService.RefreshNoteAsync(note);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await noteService.DeleteNoteAsync(id);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> EditTitle(int id, [FromBody] EditTitleModel model)
        {
            var note = await noteService.GetNoteByIdAsync(id);
            if (note == null) return NotFound();

            note.Title = model.Title;
            await noteService.RefreshNoteAsync(note);

            return Ok();
        }

        public class EditTitleModel
        {
            public string Title { get; set; } = "";
        }
    }
}
