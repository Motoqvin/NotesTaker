using Microsoft.EntityFrameworkCore;
using NotesTakerApp.Core.Models;

namespace NotesTakerApp.Infrastructure.Data;

public class NotesTakerSqlServerDbContext : DbContext
{
    public DbSet<Note> Notes { get; set; }
    public DbSet<User> Users { get; set; }
    public NotesTakerSqlServerDbContext(DbContextOptions<NotesTakerSqlServerDbContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}