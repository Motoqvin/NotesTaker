using Microsoft.EntityFrameworkCore;
using NotesTakerApp.Core.Models;
using NotesTakerApp.Core.Repositories;
using NotesTakerApp.Infrastructure.Data;
namespace NotesTakerApp.Infrastructure.Repositories;

public class NotePostgreSqlRepository : INoteRepository
{
    private readonly NotesTakerNoteDbContext DbContext;

    public NotePostgreSqlRepository(NotesTakerNoteDbContext dbContext)
    {
        DbContext = dbContext;
    }
    public Task CreateNoteAsync(Note note)
    {
        DbContext.Notes.Add(note);
        return DbContext.SaveChangesAsync();
    }

    public Task DeleteNoteAsync(int id)
    {
        var note = DbContext.Notes.Find(id);
        if (note == null)
        {
            throw new ArgumentNullException(nameof(note), "Note not found");
        }
        DbContext.Notes.Remove(note);
        return DbContext.SaveChangesAsync();
    }

    public Task<List<Note>> GetAllNotesAsync(int userId)
    {
        return DbContext.Notes.Where(n => n.Id == userId).ToListAsync();
    }

    public Task<Note> GetNoteByIdAsync(int id)
    {
        var note = DbContext.Notes.Find(id);
        if (note == null)
        {
            throw new ArgumentNullException(nameof(note), "Note not found");
        }
        return Task.FromResult(note);
    }

    public Task UpdateNoteAsync(Note note)
    {
        DbContext.Notes.Update(note);
        return DbContext.SaveChangesAsync();
    }
}