using NotesTakerApp.Core.Models;
using NotesTakerApp.Core.Repositories;

namespace NotesTakerApp.Infrastructure.Repositories;

public class UserMSSqlRepository : IUserRepository
{
    public Task CreateUserAsync(User user)
    {
        throw new NotImplementedException();
    }

    public Task DeleteUserAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<User?> GetUserByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateUserAsync(User user)
    {
        throw new NotImplementedException();
    }
}