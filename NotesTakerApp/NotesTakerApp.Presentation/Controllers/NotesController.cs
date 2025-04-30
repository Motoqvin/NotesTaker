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
        public async Task<IActionResult> Index()
        {
            var notes = noteService.GetAllNotesAsync();
            return View(notes);
        }
        public IActionResult Index(string search)
        {
            var notes = noteService.GetAllNotesAsync();

            if (!string.IsNullOrWhiteSpace(search))
            {
                notes = (List<Note>)notes.Where(n => n.Title != null && n.Title.Contains(search, StringComparison.OrdinalIgnoreCase));
            }

            return View(notes);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var note = await noteService.GetNoteByIdAsync(id);
            return View(note);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Create a new note
        [HttpPost]
        public async Task<IActionResult> Create(string Title)
        {
            if (string.IsNullOrEmpty(Title))
            {
                return RedirectToAction(nameof(Index)); // If no title, just return to the index
            }

            var newNote = new Note
            {
                Title = Title
            };

            await noteService.PostNoteAsync(newNote);

            return RedirectToAction(nameof(Index));
        }

        // POST: Update an existing note's content
        [HttpPost]
        public async Task<IActionResult> Edit(int Id, string Content)
        {
            var note = await noteService.GetNoteByIdAsync(Id);
            if (note == null)
            {
                return NotFound(); // If the note is not found
            }

            note.Content = Content;
            await noteService.RefreshNoteAsync(note);

            return RedirectToAction(nameof(Index)); // Redirect back to the index page
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var note = await noteService.GetNoteByIdAsync(id);
            return View(note);
        }

        [HttpDelete, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await noteService.DeleteNoteAsync(id);
            return RedirectToAction(nameof(Index)); // Redirect to the index page after deletion
        }
    }
}
