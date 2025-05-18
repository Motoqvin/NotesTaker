using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotesTakerApp.Core.Models;
using NotesTakerApp.Core.Repositories;
using NotesTakerApp.Infrastructure.Data;

namespace NotesTakerApp.Presentation.Controllers;

[Authorize]
public class NotesController : Controller
{
    private readonly INoteRepository _noteRepository;
    private readonly UserManager<User> _userManager;
    private readonly UsersIdentityDb _context;

    public NotesController(INoteRepository noteRepository, UserManager<User> userManager, UsersIdentityDb context)
    {
        _noteRepository = noteRepository;
        _userManager = userManager;
        _context = context;
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
        var notesQuery = _noteRepository.GetAllNotesAsync(userId);

        List<Note> notes = notesQuery;

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
        if (note == null)
            return NotFound();

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
        if (note == null)
            return NotFound();

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
        if (string.IsNullOrWhiteSpace(model.Title))
            return BadRequest("Title cannot be empty.");

        var note = await _noteRepository.GetNoteByIdAsync(id);
        if (note == null)
            return NotFound();

        note.Title = model.Title;
        await _noteRepository.UpdateNoteAsync(note);

        return Ok();
    }

    public class EditTitleModel
    {
        public string Title { get; set; } = "";
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var currentUser = await _userManager.GetUserAsync(User);

        var users = await _userManager.Users
            .Where(u => u.Id != currentUser.Id)
            .Select(u => new { u.Id, u.UserName, u.Email })
            .ToListAsync();

        return Json(users);
    }

    [HttpPost]
    public async Task<IActionResult> Share([FromBody] ShareNoteRequest request)
    {
        var note = await _context.Notes
            .Include(n => n.SharedWith)
            .FirstOrDefaultAsync(n => n.Id == request.NoteId);

        var userToShareWith = await _userManager.FindByIdAsync(request.UserId);

        if (note == null || userToShareWith == null)
            return NotFound();

        if (!note.SharedWith.Any(u => u.Id == userToShareWith.Id))
        {
            note.SharedWith.Add(userToShareWith);
            await _context.SaveChangesAsync();
        }

        return Ok();
    }
    [HttpGet]
    public async Task<IActionResult> SharedWithMe()
    {
        var userId = await GetUserIdAsync();
        var notes = await _noteRepository.GetAllSharedWithUserAsync(userId);
        return View("Index", notes); // Or a separate view if you prefer
    }

    [HttpGet]
public async Task<IActionResult> GetAllUsersExceptOwners(int noteId)
{
    var currentUser = await _userManager.GetUserAsync(User);

    var note = await _context.Notes
        .Include(n => n.User)
        .Include(n => n.SharedWith)
        .FirstOrDefaultAsync(n => n.Id == noteId);

    if (note == null)
        return NotFound("Note not found");

    var sharedUserIds = note.SharedWith.Select(u => u.Id).ToHashSet();

    var users = await _userManager.Users
        .Where(u => u.Id != currentUser.Id && u.Id != note.User.Id && !sharedUserIds.Contains(u.Id))
        .Select(u => new { u.Id, u.UserName, u.Email })
        .ToListAsync();

    return Json(users);
}


    public class ShareNoteRequest
    {
        public int NoteId { get; set; }
        public string UserId { get; set; } = string.Empty;
    }
}
