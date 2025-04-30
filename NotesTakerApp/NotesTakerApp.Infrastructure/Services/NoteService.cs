using NotesTakerApp.Core.Models;
using NotesTakerApp.Core.Repositories;
using NotesTakerApp.Core.Services;

namespace NotesTakerApp.Infrastructure.Services;

public class NoteService : INoteService
{
    private readonly INoteRepository noteRepository;
    public NoteService(INoteRepository noteRepository)
    {
        this.noteRepository = noteRepository;
    }
    public Task DeleteNoteAsync(int id)
    {
        noteRepository.DeleteNoteAsync(id);
        return Task.CompletedTask;
    }

    public List<Note> GetAllNotesAsync()
    {
        var notes = noteRepository.GetAllNotesAsync();
        return notes;
    }

    public Task<Note> GetNoteByIdAsync(int id)
    {
        var note = noteRepository.GetNoteByIdAsync(id);
        return note;
    }

    public Task PostNoteAsync(Note note)
    {
        noteRepository.CreateNoteAsync(note.Title);
        return Task.CompletedTask;
    }

    public Task RefreshNoteAsync(Note note)
    {
        noteRepository.UpdateNoteAsync(note);
        return Task.CompletedTask;
    }
}