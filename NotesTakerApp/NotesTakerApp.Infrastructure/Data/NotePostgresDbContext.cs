using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using NotesTakerApp.Core.Models;

namespace NotesTakerApp.Infrastructure.Data;

public class NotePostgresDbContext : DbContext
{
    public DbSet<Note> Notes { get; set; }

    public NotePostgresDbContext(DbContextOptions<NotePostgresDbContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            // Читаем appsettings.json вручную для миграций
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) // Текущая папка
                .AddJsonFile("appsettings.json")               // appsettings.json
                .Build();

            var connectionString = configuration.GetConnectionString("NotesTakerAppPostgreSqlServerContext");

            optionsBuilder.UseNpgsql(connectionString);
        }

        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
