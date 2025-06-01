namespace NotesTakerApp.Core.Models;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

public class Note
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public string Content { get; set; } = string.Empty;
    public bool IsPinned { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string UserId { get; set; } = string.Empty;
    [ForeignKey("UserId")]
    public User Owner { get; set; } = null!;
    public ICollection<User> SharedWithUsers { get; set; } = new List<User>();
    public ICollection<NoteContributor> Contributors { get; set; } = new List<NoteContributor>();
}