namespace NotesTakerApp.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
public class User : IdentityUser
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public List<string> Roles{ get; set; }
}