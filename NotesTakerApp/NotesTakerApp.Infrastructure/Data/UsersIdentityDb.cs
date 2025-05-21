using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NotesTakerApp.Core.Models;

namespace NotesTakerApp.Infrastructure.Data;

public class UsersIdentityDb : IdentityDbContext<User, IdentityRole, string>
{
    public DbSet<Note> Notes { get; set; }
    public DbSet<NoteContributor> NoteContributors { get; set; }
    public UsersIdentityDb(DbContextOptions<UsersIdentityDb> options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

    builder.Entity<Note>()
        .HasOne(n => n.User)
        .WithMany()
        .HasForeignKey(n => n.UserId)
        .OnDelete(DeleteBehavior.Cascade);

    builder.Entity<Note>()
        .HasMany(n => n.SharedWith)
        .WithMany("SharedNotes");

    builder.Entity<NoteContributor>()
        .HasKey(nc => nc.Id);

    builder.Entity<NoteContributor>()
        .HasOne(nc => nc.Note)
        .WithMany(n => n.Contributors)
        .HasForeignKey(nc => nc.NoteId);

    builder.Entity<NoteContributor>()
        .HasOne(nc => nc.User)
        .WithMany()
        .HasForeignKey(nc => nc.UserId);
    }
}