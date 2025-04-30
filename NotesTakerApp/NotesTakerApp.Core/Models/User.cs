namespace NotesTakerApp.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
public class User : IdentityUser
{
    public List<string> Roles{ get; set; }
}