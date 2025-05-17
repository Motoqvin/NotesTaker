using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Back.Models;

namespace Back.Data;

public class UsersIdentityDb : IdentityDbContext<User, IdentityRole, string>
{
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public UsersIdentityDb(DbContextOptions<UsersIdentityDb> options) : base(options) { }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder
            .Entity<RefreshToken>()
            .HasKey(rt => rt.Token);
    }
}