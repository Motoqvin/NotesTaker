// Fixed NoteSqlRepository
using Microsoft.EntityFrameworkCore;
using NotesTakerApp.Core.Models;
using NotesTakerApp.Core.Repositories;
using NotesTakerApp.Infrastructure.Data;

namespace NotesTakerApp.Infrastructure.Repositories;

public class NoteSqlRepository : INoteRepository
{
    private readonly UsersIdentityDb _dbContext;

    public NoteSqlRepository(UsersIdentityDb dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CreateNoteAsync(string title, string userId)
    {
        var note = new Note
        {
            Title = title,
            Content = "",
            CreatedAt = DateTime.UtcNow,
            UserId = userId
        };
        _dbContext.Notes.Add(note);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteNoteAsync(int id)
    {
        var note = await _dbContext.Notes.FindAsync(id);
        if (note == null)
        {
            throw new InvalidOperationException("Note not found");
        }
        _dbContext.Notes.Remove(note);
        await _dbContext.SaveChangesAsync();
    }


    public List<Note> GetAllNotesAsync(string userId)
    {
        return _dbContext.Notes
            .Where(n => n.UserId == userId)
            .ToList();
    }

    public async Task<List<Note>> GetNotesWhereUserIsContributorAsync(string userId)
    {
        return await _dbContext.Notes
            .Include(n => n.Owner) // Changed from n.User to n.Owner
            .Include(n => n.Contributors)
            .Where(n => n.Contributors.Any(c => c.UserId == userId))
            .ToListAsync();
    }

    public async Task<Note> GetNoteByIdAsync(int id)
    {
        var note = await _dbContext.Notes.FindAsync(id);
        if (note == null)
        {
            throw new InvalidOperationException("Note not found");
        }
        return note;
    }

    public async Task UpdateNoteAsync(Note note)
    {
        _dbContext.Notes.Update(note);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Note> GetNoteWithContributorsAsync(int noteId)
    {
        var note = await _dbContext.Notes
            .Include(n => n.Contributors)
            .ThenInclude(c => c.User)
            .FirstOrDefaultAsync(n => n.Id == noteId);

        if (note == null)
        {
            throw new InvalidOperationException("Note not found");
        }

        return note;
    }
    public async Task<bool> AddContributorAsync(int noteId, string userId)
    {
        var note = await _dbContext.Notes
            .Include(n => n.Contributors)
            .FirstOrDefaultAsync(n => n.Id == noteId);

        if (note == null)
        {
            return false;
        }

        if (note.Contributors.Any(c => c.UserId == userId))
        {
            return false;
        }


        var userExists = await _dbContext.Users.AnyAsync(u => u.Id == userId);
        if (!userExists)
        {
            return false;
        }

        var contributor = new NoteContributor
        {
            NoteId = noteId,
            UserId = userId
        };

        note.Contributors.Add(contributor);
        await _dbContext.SaveChangesAsync();
        return true;
    }
    public async Task<bool> AddContributorsAsync(int noteId, List<string> userIds)
    {
        var note = await _dbContext.Notes
            .Include(n => n.Contributors)
            .FirstOrDefaultAsync(n => n.Id == noteId);

        if (note == null)
            return false;

        var existingUserIds = note.Contributors.Select(c => c.UserId).ToHashSet();

        var newContributors = userIds
            .Where(userId => !existingUserIds.Contains(userId))
            .Select(userId => new NoteContributor
            {
                NoteId = noteId,
                UserId = userId
            }).ToList();

        if (newContributors.Any())
        {
            foreach (var contributor in newContributors)
            {
                note.Contributors.Add(contributor);
            }
            await _dbContext.SaveChangesAsync();
        }

        return true;
    }



    public async Task<bool> RemoveContributorAsync(int noteId, string userId)
    {
        var contributor = await _dbContext.Set<NoteContributor>()
            .FirstOrDefaultAsync(c => c.NoteId == noteId && c.UserId == userId);

        if (contributor == null)
        {
            return false;
        }

        _dbContext.Set<NoteContributor>().Remove(contributor);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> IsUserContributorAsync(int noteId, string userId)
    {
        return await _dbContext.Set<NoteContributor>()
            .AnyAsync(c => c.NoteId == noteId && c.UserId == userId);
    }

    public async Task<List<User>> GetAvailableUsersForNoteAsync(int noteId, string currentUserId)
    {
        var note = await _dbContext.Notes
            .Include(n => n.Contributors)
            .FirstOrDefaultAsync(n => n.Id == noteId);

        if (note == null)
        {
            return new List<User>();
        }

        var contributorUserIds = note.Contributors.Select(c => c.UserId).ToHashSet();

        return await _dbContext.Users
            .Where(u => u.Id != currentUserId &&
                       u.Id != note.UserId &&
                       !contributorUserIds.Contains(u.Id))
            .ToListAsync();
    }

}