using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NotesTakerApp.Core.Models;

namespace NotesTakerApp.Infrastructure.Data;
public class UsersIdentityDb : IdentityDbContext<IdentityUser, IdentityRole, string>
{
    public UsersIdentityDb(DbContextOptions<UsersIdentityDb> options) : base(options) { }
}