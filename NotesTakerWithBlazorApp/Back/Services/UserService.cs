using Back.Models;
using Back.Repositories.Base;
using Back.Services.Base;

namespace Back.Services;

public class UserService : IUserService
{
    private readonly IUserRepository userRepository;

    public UserService(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }
    public Task AddUserAsync(User user)
    {
        userRepository.CreateUserAsync(user);
        return Task.CompletedTask;
    }

    public Task DeleteUserAsync(int id)
    {
        userRepository.DeleteUserAsync(id);
        return Task.CompletedTask;
    }

    public async Task<List<User>> GetAllUsersAsync()
    {
        return await userRepository.GetAllUsersAsync();
    }

    public Task<User> GetUserByIdAsync(int id)
    {
        var user = userRepository.GetUserByIdAsync(id);
        return user;
    }

    public Task UpdateUserAsync(User user)
    {
        userRepository.UpdateUserAsync(user);
        return Task.CompletedTask;
    }
}