namespace NotesTakerApp.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
public class User : IdentityUser
{
    public int Id { get; set; }
    public required string Email { get; set; }
    public required string Username { get; set; }
    public string? Password { get; set; }
}