using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NotesTakerApp.Core.Models;

namespace NotesTakerApp.Infrastructure.Data
{
    public class UsersIdentityDb : IdentityDbContext<User, IdentityRole, string>
    {
        public UsersIdentityDb(DbContextOptions<UsersIdentityDb> options) : base(options)
        {
        }

        public DbSet<Note> Notes { get; set; }
        public DbSet<NoteContributor> NoteContributors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Note>(entity =>
            {
                entity.HasKey(n => n.Id);

                entity.Property(n => n.Title)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(n => n.Content)
                    .HasMaxLength(10000);

                entity.Property(n => n.UserId)
                    .IsRequired();

                entity.HasOne(n => n.Owner)
                    .WithMany(u => u.OwnedNotes)
                    .HasForeignKey(n => n.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(n => n.SharedWithUsers)
                    .WithMany(u => u.SharedNotes)
                    .UsingEntity<Dictionary<string, object>>(
                        "NoteSharedUsers",
                        j => j
                            .HasOne<User>()
                            .WithMany()
                            .HasForeignKey("UserId")
                            .OnDelete(DeleteBehavior.Cascade),
                        j => j
                            .HasOne<Note>()
                            .WithMany()
                            .HasForeignKey("NoteId")
                            .OnDelete(DeleteBehavior.Cascade),
                        j =>
                        {
                            j.HasKey("NoteId", "UserId");
                            j.ToTable("NoteSharedUsers");
                        });
            });

            
        }
    }
}
