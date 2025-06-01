using NotesTakerApp.Core.Models;

namespace NotesTakerApp.Core.Services;

public interface INoteService
{
    Task<Note> GetNoteByIdAsync(int id);
    Task PostNoteAsync(Note note);
    Task DeleteNoteAsync(int id);
    Task RefreshNoteAsync(Note note);
    List<Note> GetAllNotesAsync();
}