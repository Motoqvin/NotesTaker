using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Back.Data
{
    public class UsersIdentityDbFactory : IDesignTimeDbContextFactory<UsersIdentityDb>
    {
        public UsersIdentityDb CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../NotesTakerApp.Presentation"))
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<UsersIdentityDb>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("NotesTakerAppSqlServerContext"));

            return new UsersIdentityDb(optionsBuilder.Options);
        }
    }
}