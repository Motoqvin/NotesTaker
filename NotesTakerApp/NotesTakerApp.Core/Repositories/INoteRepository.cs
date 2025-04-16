using NotesTakerApp.Core.Models;

namespace NotesTakerApp.Core.Repositories;
public interface INoteRepository
{
    public Task<Note> GetNoteByIdAsync(int id);
    public Task CreateNoteAsync(Note note);
    public Task DeleteNoteAsync(int id);
    public Task UpdateNoteAsync(Note note);
    public Task<IEnumerable<Note>> GetAllNotesAsync(int userId);
}