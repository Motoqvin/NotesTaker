using Microsoft.EntityFrameworkCore;
using NotesTakerApp.Core.Models;

namespace NotesTakerApp.Infrastructure.Data;

public class NotesTakerNoteDbContext : DbContext
{
    
    public DbSet<Note> Notes { get; set; }
    public NotesTakerNoteDbContext(DbContextOptions<NotesTakerSqlServerDbContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        base.OnModelCreating(modelBuilder);
    }
}