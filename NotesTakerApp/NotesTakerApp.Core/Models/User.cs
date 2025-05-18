namespace NotesTakerApp.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
public class User : IdentityUser
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public ICollection<Note> Notes { get; set; } = new List<Note>();
    public string PasswordHash { get; set; }
    public List<string> Roles{ get; set; }
}