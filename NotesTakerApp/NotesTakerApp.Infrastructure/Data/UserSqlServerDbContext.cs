using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NotesTakerApp.Core.Models;

namespace NotesTakerApp.Infrastructure.Data;

public class UserSqlServerDbContext : DbContext{
    
    public DbSet<User> Users { get; set; }
    public UserSqlServerDbContext(DbContextOptions<UserSqlServerDbContext> options) : base(options) {}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("NotesTakerAppSqlServerContext");

            optionsBuilder.UseSqlServer(connectionString);
        }

        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
    }

