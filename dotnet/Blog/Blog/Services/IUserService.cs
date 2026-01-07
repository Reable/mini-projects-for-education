using Blog.Models;

namespace Blog.Services;

public interface IUserService
{
    Task<List<User>> GetUsersAsync();
    Task<User?> GetUserAsync(string id);
    Task<User> CreateUserAsync(User user);
}