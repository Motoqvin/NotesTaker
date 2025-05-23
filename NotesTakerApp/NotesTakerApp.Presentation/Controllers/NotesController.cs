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
        var userId = await GetUserIdAsync();
    var note = await _context.Notes
        .Include(n => n.Contributors)
        .FirstOrDefaultAsync(n => n.Id == id);

    if (note == null)
        return NotFound();

    if (note.UserId != userId && !note.Contributors.Any(c => c.UserId == userId))
        return Forbid();

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
        if (note == null) return NotFound();

        note.Title = model.Title;
        await _noteRepository.UpdateNoteAsync(note);

        return Ok();
    }

    public class EditTitleModel
    {
        public string Title { get; set; } = "";
    }

    [HttpPost]
public async Task<IActionResult> AddContributor([FromBody] AddContributorRequest request)
{
    var note = await _context.Notes
        .Include(n => n.Contributors)
        .FirstOrDefaultAsync(n => n.Id == request.NoteId);

    if (note == null) return NotFound("Note not found");

    var user = await _userManager.FindByIdAsync(request.UserId);
    if (user == null) return NotFound("User not found");

    bool alreadyAdded = note.Contributors.Any(c => c.UserId == user.Id);
    if (!alreadyAdded)
    {
        note.Contributors.Add(new NoteContributor
        {
            NoteId = note.Id,
            UserId = user.Id
        });
        await _context.SaveChangesAsync();
    }

    return Ok();
}

[HttpGet]
public async Task<IActionResult> SharedWithMeAsContributor()
{
    var userId = await GetUserIdAsync();
    var notes = await _noteRepository.GetNotesWhereUserIsContributorAsync(userId);
    return View("Index", notes);
}
    public class AddContributorRequest
    {
        public int NoteId { get; set; }
        public string UserId { get; set; } = string.Empty;
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

        var users = await _context.Users
            .Where(u => u.Id != currentUser.Id && u.Id != note.User.Id && !sharedUserIds.Contains(u.Id))
            .Select(u => new { u.Id, u.UserName, u.Email })
            .ToListAsync();
        ViewData["NoteTitle"] = note.Title;
        ViewData["NoteId"] = note.Id;
        return Json(users);
    }
    [HttpGet]
    public async Task<IActionResult> Contributors(int noteId)
    {
        var note = await _context.Notes
            .Include(n => n.Contributors)
            .ThenInclude(nu => nu.User)
            .FirstOrDefaultAsync(n => n.Id == noteId);

        if (note == null) return NotFound();

        var contributors = note.Contributors.Select(nu => new
        {
            Id = nu.User.Id,
            UserName = nu.User.UserName,
            Email = nu.User.Email
        });

        ViewData["NoteTitle"] = note.Title;
        return View(contributors);
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

    public class ShareNoteRequest
    {
        public int NoteId { get; set; }
        public string UserId { get; set; } = string.Empty;
    }
}
