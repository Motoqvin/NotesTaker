using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Back.Data;
using System.IO;

namespace Back.Data
{
    public class NotePostgresDbContextFactory : IDesignTimeDbContextFactory<NotePostgresDbContext>
    {
        public NotePostgresDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../NotesTakerApp.Presentation"))
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<NotePostgresDbContext>();
            optionsBuilder.UseNpgsql(configuration.GetConnectionString("NotesTakerAppPostgreSqlServerContext"));

            return new NotePostgresDbContext(optionsBuilder.Options);
        }
    }
}
