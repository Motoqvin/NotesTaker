using Microsoft.EntityFrameworkCore;
using NotesTakerApp.Core.Models;

namespace NotesTakerApp.Infrastructure.Data;

public class UserSqlServerDbContext : DbContext
{
    
    public DbSet<User> Users { get; set; }
    public UserSqlServerDbContext(DbContextOptions<UserSqlServerDbContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        base.OnModelCreating(modelBuilder);
    }
}