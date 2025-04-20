using NotesTakerApp.Core.Models;
using NotesTakerApp.Core.Repositories;

namespace NotesTakerApp.Infrastructure.Repositories;

public class NoteMSSqlRepository : INoteRepository
{
    public Task CreateNoteAsync(Note note)
    {
        throw new NotImplementedException();
    }

    public Task DeleteNoteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Note>> GetAllNotesAsync(int userId)
    {
        throw new NotImplementedException();
    }

    public Task<Note> GetNoteByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateNoteAsync(Note note)
    {
        throw new NotImplementedException();
    }
}