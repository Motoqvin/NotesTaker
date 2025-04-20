using NotesTakerApp.Core.Models;
using NotesTakerApp.Core.Services;

namespace NotesTakerApp.Infrastructure.Services;

public class NoteService : INoteService
{
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

    public Task PostNoteAsync(Note note)
    {
        throw new NotImplementedException();
    }

    public Task RefreshNoteAsync(Note note)
    {
        throw new NotImplementedException();
    }
}