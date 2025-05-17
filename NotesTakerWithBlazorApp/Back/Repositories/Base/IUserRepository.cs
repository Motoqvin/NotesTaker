using Back.Models;

namespace Back.Repositories.Base;
public interface IUserRepository
{
    public Task<User> GetUserByIdAsync(int id);
    public Task<List<User>> GetAllUsersAsync();
    public Task CreateUserAsync(User user);
    public Task DeleteUserAsync(int id);
    public Task UpdateUserAsync(User user);
}