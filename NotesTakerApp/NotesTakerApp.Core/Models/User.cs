namespace NotesTakerApp.Core.Models;
public class User
{
    public int Id { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
    public List<Note> Notes { get; set; } = new List<Note>();
}