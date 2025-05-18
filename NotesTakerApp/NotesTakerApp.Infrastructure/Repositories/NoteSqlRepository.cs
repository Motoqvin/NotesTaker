using Microsoft.EntityFrameworkCore;
using NotesTakerApp.Core.Models;
using NotesTakerApp.Core.Repositories;
using NotesTakerApp.Infrastructure.Data;

namespace NotesTakerApp.Infrastructure.Repositories;

public class NoteSqlRepository : INoteRepository
{
    private readonly UsersIdentityDb _dbContext;

    public NoteSqlRepository(UsersIdentityDb dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CreateNoteAsync(string title, string userId)
    {
        var note = new Note
        {
            Title = title,
            Content = "",
            CreatedAt = DateTime.UtcNow,
            UserId = userId
        };

        _dbContext.Notes.Add(note);
        await _dbContext.SaveChangesAsync();
    }

    

    public async Task DeleteNoteAsync(int id)
    {
        var note = await _dbContext.Notes.FindAsync(id);
        if (note == null)
        {
            throw new ArgumentNullException(nameof(note), "Note not found");
        }

        _dbContext.Notes.Remove(note);
        await _dbContext.SaveChangesAsync();
    }

    public List<Note> GetAllNotesAsync(string userId)
    {
        return _dbContext.Notes
            .Where(n => n.UserId == userId)
            .ToList();
    }

    

    public async Task<Note> GetNoteByIdAsync(int id)
    {
        var note = await _dbContext.Notes.FindAsync(id);
        if (note == null)
        {
            throw new ArgumentNullException(nameof(note), "Note not found");
        }

        return note;
    }

    public async Task UpdateNoteAsync(Note note)
    {
        _dbContext.Notes.Update(note);
        await _dbContext.SaveChangesAsync();
    }
}
