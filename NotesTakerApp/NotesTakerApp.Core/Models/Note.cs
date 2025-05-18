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

    // Foreign Key to User
    public string UserId { get; set; }

    [ForeignKey("UserId")]
    public User User { get; set; }
}
