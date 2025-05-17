using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Back.Models;

namespace Back.Data;
public class UsersIdentityDb : IdentityDbContext<User, IdentityRole, string>
{
    public UsersIdentityDb(DbContextOptions<UsersIdentityDb> options) : base(options) { }
}