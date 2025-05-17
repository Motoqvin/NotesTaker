using Back.Models;

namespace Back.Repositories.Base;
public interface INoteRepository
{
    public Task<Note> GetNoteByIdAsync(int id);
    public Task CreateNoteAsync(string title);
    public Task DeleteNoteAsync(int id);
    public Task UpdateNoteAsync(Note note);
    public List<Note> GetAllNotesAsync();
}