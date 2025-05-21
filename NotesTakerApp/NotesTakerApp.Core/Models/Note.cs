using System.ComponentModel.DataAnnotations.Schema;

namespace NotesTakerApp.Core.Models;

public class Note
{
    public int Id { get; set; }

    public required string Title { get; set; }

    public string Content { get; set; }

    public bool IsPinned { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public string? SharedLink { get; set; }

    public string UserId { get; set; }

    public User User { get; set; }
    public ICollection<User> SharedWith { get; set; } = new List<User>();
    public List<NoteContributor> Contributors { get; set; } = new List<NoteContributor>();

}
