using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NotesTakerApp.Core.Models;

namespace NotesTakerApp.Infrastructure.Data;

public class UsersIdentityDb : IdentityDbContext<User, IdentityRole, string>
{
    public DbSet<Note> Notes { get; set; }
    public UsersIdentityDb(DbContextOptions<UsersIdentityDb> options) : base(options)
    { }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Note>()
        .HasOne(n => n.User)
        .WithMany(u => u.Notes)
        .HasForeignKey(n => n.UserId)
        .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Note>()
        .HasMany(n => n.SharedWith)
        .WithMany(u => u.SharedNotes)
        .UsingEntity(j => j.ToTable("NoteUserShares"));
    }
}