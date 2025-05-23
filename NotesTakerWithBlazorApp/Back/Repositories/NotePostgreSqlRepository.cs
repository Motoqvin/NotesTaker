using Microsoft.EntityFrameworkCore;
using Back.Models;
using Back.Repositories.Base;
using Back.Data;

namespace Back.Repositories
{
    public class NotePostgreSqlRepository : INoteRepository
    {
        private readonly NotePostgresDbContext DbContext;

        public NotePostgreSqlRepository(NotePostgresDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public async Task CreateNoteAsync(string title)
        {
            var note = new Note
                {
                    Title = title,
                    Content = "",
                    CreatedAt = DateTime.UtcNow
                };

            DbContext.Notes.Add(note);
            await DbContext.SaveChangesAsync();
        }

        public async Task DeleteNoteAsync(int id)
        {
            var note = await DbContext.Notes.FindAsync(id);
            if (note == null)
            {
                throw new ArgumentNullException(nameof(note), "Note not found");
            }

            DbContext.Notes.Remove(note);
            await DbContext.SaveChangesAsync();
        }

        public List<Note> GetAllNotesAsync()
        {
            var notes = DbContext.Notes.ToList();
            return notes;
        }

        public async Task<Note> GetNoteByIdAsync(int id)
        {
            var note = await DbContext.Notes.FindAsync(id);
            if (note == null)
            {
                throw new ArgumentNullException(nameof(note), "Note not found");
            }

            return note;
        }

        public async Task UpdateNoteAsync(Note note)
        {
            DbContext.Notes.Update(note);
            await DbContext.SaveChangesAsync();
        }
    }
}
