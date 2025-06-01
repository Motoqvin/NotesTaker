using NotesTakerApp.Core.Models;

namespace NotesTakerApp.Core.Repositories;
public interface IUserRepository
{
    public Task<User> GetUserByIdAsync(string id);
    public List<User> GetAllUsersAsync();
    public Task CreateUserAsync(User user, string password);
    public Task DeleteUserAsync(string id);
    public Task UpdateUserAsync(User user);
}