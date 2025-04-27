using Microsoft.EntityFrameworkCore;
using NotesTakerApp.Core.Models;

namespace NotesTakerApp.Infrastructure.Data;

public class NotePostgresDbContext : DbContext
{
    
    public DbSet<Note> Notes { get; set; }
    public NotePostgresDbContext(DbContextOptions<NotePostgresDbContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        base.OnModelCreating(modelBuilder);
    }
}