using Microsoft.AspNetCore.Mvc;
using NotesTakerApp.Core.Models;
using NotesTakerApp.Core.Repositories;

namespace NotesTakerApp.Presentation.Controllers
{
    public class NotesController : Controller
    {
        private readonly INoteRepository _noteRepository;

        public NotesController(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int userId)
        {
            var notes = await _noteRepository.GetAllNotesAsync(userId);
            return View(notes);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var note = await _noteRepository.GetNoteByIdAsync(id);
            return View(note);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        
        public async Task<IActionResult> Create(Note note)
        {
            if (ModelState.IsValid)
            {
                await _noteRepository.CreateNoteAsync(note);
                return RedirectToAction(nameof(Index), new { userId = note.Id });
            }
            return View(note);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var note = await _noteRepository.GetNoteByIdAsync(id);
            return View(note);
        }

        [HttpPost]

        public async Task<IActionResult> Edit(Note note)
        {
            if (ModelState.IsValid)
            {
                await _noteRepository.UpdateNoteAsync(note);
                return RedirectToAction(nameof(Index), new { userId = note.Id });
            }
            return View(note);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var note = await _noteRepository.GetNoteByIdAsync(id);
            return View(note);
        }

        [HttpPost, ActionName("Delete")]
        
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _noteRepository.DeleteNoteAsync(id);
            return RedirectToAction(nameof(Index), new { userId = 1 });
        }
    }
}
