using NotesTakerApp.Core.Models;

namespace NotesTakerApp.Core.Repositories;

public interface INoteRepository
{
    public Task<Note> GetNoteByIdAsync(int id);
    public Task CreateNoteAsync(string title, string userId);
    public Task DeleteNoteAsync(int id);
    public Task UpdateNoteAsync(Note note);
    public List<Note> GetAllNotesAsync(string userId);
    public Task<List<Note>> GetNotesWhereUserIsContributorAsync(string userId);
}