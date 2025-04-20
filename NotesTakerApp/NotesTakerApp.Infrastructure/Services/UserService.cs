using NotesTakerApp.Core.Models;
using NotesTakerApp.Core.Services;

namespace NotesTakerApp.Infrastructure.Services;

public class UserService : IUserService
{
    public Task AddUserAsync(User user)
    {
        throw new NotImplementedException();
    }

    public Task DeleteUserAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<User>> GetAllUsersAsync()
    {
        throw new NotImplementedException();
    }

    public Task<User> GetUserByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateUserAsync(User user)
    {
        throw new NotImplementedException();
    }
}