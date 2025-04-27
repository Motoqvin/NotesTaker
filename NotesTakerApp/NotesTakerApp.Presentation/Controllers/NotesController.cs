using Microsoft.AspNetCore.Mvc;
using NotesTakerApp.Core.Models;
using NotesTakerApp.Core.Repositories;
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
        public async Task<IActionResult> Index(int userId)
        {
            var notes = await noteService.GetAllNotesAsync(userId);
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

        [HttpPost]
        
        public async Task<IActionResult> Create(Note note)
        {
            if (ModelState.IsValid)
            {
                await noteService.PostNoteAsync(note);
                return RedirectToAction(nameof(Index), new { userId = note.Id });
            }
            return View(note);
        }

        [HttpPut]
        public async Task<IActionResult> Edit(int id)
        {
            var note = await noteService.GetNoteByIdAsync(id);
            return View(note);
        }

        [HttpPut]

        public async Task<IActionResult> Edit(Note note)
        {
            if (ModelState.IsValid)
            {
                await noteService.RefreshNoteAsync(note);
                return RedirectToAction(nameof(Index), new { userId = note.Id });
            }
            return View(note);
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
            return RedirectToAction(nameof(Index), new { userId = 1 });
        }
    }
}
