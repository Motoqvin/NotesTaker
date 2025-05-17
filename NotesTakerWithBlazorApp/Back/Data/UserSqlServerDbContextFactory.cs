using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Back.Data
{
    public class UserSqlServerDbContextFactory : IDesignTimeDbContextFactory<UserSqlServerDbContext>
    {
        public UserSqlServerDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../NotesTakerApp.Presentation"))
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<UserSqlServerDbContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("NotesTakerAppSqlServerContext"));

            return new UserSqlServerDbContext(optionsBuilder.Options);
        }

        
    }
    }
