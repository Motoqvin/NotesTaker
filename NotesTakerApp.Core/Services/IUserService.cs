using NotesTakerApp.Core.Models;

namespace NotesTakerApp.Core.Services;

public interface IUserService
{
    Task<User> GetUserByIdAsync(int id);
    Task AddUserAsync(User user);
    Task DeleteUserAsync(int id);
    Task UpdateUserAsync(User user);
    Task<List<User>> GetAllUsersAsync();
}