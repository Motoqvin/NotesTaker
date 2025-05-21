namespace NotesTakerApp.Core.Models;
public class NoteContributor
{
    public int Id { get; set; }

    public int NoteId { get; set; }
    public Note Note { get; set; } = null!;

    public string UserId { get; set; }
    public User User { get; set; } = null!;
}
