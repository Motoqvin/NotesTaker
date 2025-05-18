namespace NotesTakerApp.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
public class User : IdentityUser
{
    public ICollection<Note> Notes { get; set; } = new List<Note>();
    public ICollection<Note> SharedNotes { get; set; } = new List<Note>();
    public List<string> Roles { get; set; } = new List<string> { "User" };
}
