using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NotesTakerApp.Core.Models;
using NotesTakerApp.Core.Repositories;

namespace NotesTakerApp.Presentation.Controllers;

[Authorize]
public class NotesController : Controller
{
    private readonly INoteRepository _noteRepository;
    private readonly UserManager<User> _userManager;

    public NotesController(INoteRepository noteRepository, UserManager<User> userManager)
    {
        _noteRepository = noteRepository;
        _userManager = userManager;
    }

    private async Task<string> GetUserIdAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        return user?.Id ?? throw new Exception("User not logged in");
    }

    [HttpGet]
    public async Task<IActionResult> Index(string? search)
    {
        var userId = await GetUserIdAsync();
        var notes = _noteRepository.GetAllNotesAsync(userId);

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
        var note = await _noteRepository.GetNoteByIdAsync(id);
        if (note == null) return NotFound();

        return View(note);
    }

    [HttpPost]
    public async Task<IActionResult> Create(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            return RedirectToAction(nameof(Index));

        var userId = await GetUserIdAsync();
        await _noteRepository.CreateNoteAsync(title, userId);

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, string content)
    {
        var note = await _noteRepository.GetNoteByIdAsync(id);
        if (note == null) return NotFound();

        note.Content = content;
        await _noteRepository.UpdateNoteAsync(note);

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        await _noteRepository.DeleteNoteAsync(id);
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> EditTitle(int id, [FromBody] EditTitleModel model)
    {
        var note = await _noteRepository.GetNoteByIdAsync(id);
        if (note == null) return NotFound();

        note.Title = model.Title;
        await _noteRepository.UpdateNoteAsync(note);

        return Ok();
    }

    public class EditTitleModel
    {
        public string Title { get; set; } = "";
    }
}
