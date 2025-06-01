using NotesTakerApp.Core.Models;

namespace NotesTakerApp.Core.Repositories;

public interface INoteRepository
{
Task CreateNoteAsync(string title, string userId);
    Task<Note> GetNoteByIdAsync(int id);
    List<Note> GetAllNotesAsync(string userId);
    Task UpdateNoteAsync(Note note);
    Task DeleteNoteAsync(int id);
    Task<List<Note>> GetNotesWhereUserIsContributorAsync(string userId);
    Task<Note> GetNoteWithContributorsAsync(int noteId);
    Task<bool> AddContributorAsync(int noteId, string userId);
    Task<bool> AddContributorsAsync(int noteId, List<string> userIds);
    Task<bool> RemoveContributorAsync(int noteId, string userId);
    Task<bool> IsUserContributorAsync(int noteId, string userId);
    Task<List<User>> GetAvailableUsersForNoteAsync(int noteId, string currentUserId);

}