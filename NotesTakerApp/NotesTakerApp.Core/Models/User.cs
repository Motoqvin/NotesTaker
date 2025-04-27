namespace NotesTakerApp.Core.Models;
public class User
{
    public int Id { get; set; }
    public required string Email { get; set; }
    public required string Username { get; set; }
    public string? Password { get; set; }
}