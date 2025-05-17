using Microsoft.EntityFrameworkCore;
using Back.Models;
using Back.Repositories.Base;
using Back.Data;

namespace Back.Repositories;

public class UserMSSqlRepository : IUserRepository
{
    private readonly UserSqlServerDbContext DbContext;

    public UserMSSqlRepository(UserSqlServerDbContext dbContext)
    {
        DbContext = dbContext;
    }
    public Task CreateUserAsync(User user)
    {
        DbContext.Database.EnsureCreated();
        DbContext.Users.Add(user);
        return DbContext.SaveChangesAsync();
    }

    public Task DeleteUserAsync(int id)
    {
        DbContext.Database.EnsureCreated();
        var user = DbContext.Users.Find(id);
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user), "User not found");
        }
        DbContext.Users.Remove(user);
        return DbContext.SaveChangesAsync();
    }

    public Task<User> GetUserByIdAsync(int id)
    {
        DbContext.Database.EnsureCreated();
        var user = DbContext.Users.Find(id);
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user), "User not found");
        }
        return Task.FromResult(user);
    }

    public Task UpdateUserAsync(User user)
    {
        DbContext.Database.EnsureCreated();
        DbContext.Users.Update(user);
        return DbContext.SaveChangesAsync();
    }

    public Task<List<User>> GetAllUsersAsync()
    {
        DbContext.Database.EnsureCreated();
        return DbContext.Users.ToListAsync();
    }
}