using NotesTakerApp.Core.Models;

namespace NotesTakerApp.Core.Repositories;
public interface IUserRepository
{
    public Task<User?> GetUserByIdAsync(int id);
    public Task CreateUserAsync(User user);
    public Task DeleteUserAsync(int id);
    public Task UpdateUserAsync(User user);
}