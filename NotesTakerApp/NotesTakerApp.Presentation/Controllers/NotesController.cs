using Microsoft.AspNetCore.Mvc;
using NotesTakerApp.Core.Models;
using NotesTakerApp.Core.Services;

namespace NotesTakerApp.Presentation.Controllers
{
    public class NotesController : Controller
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

            return View(notes);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var note = await noteService.GetNoteByIdAsync(id);
            if (note == null) return NotFound();
            return View(note);
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

        // ✅ Support Delete via POST (to match JS)
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await noteService.DeleteNoteAsync(id);
            return Ok(); // Return 200 for JS to confirm deletion
        }

        // ✅ Support EditTitle via POST (to match JS)
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
