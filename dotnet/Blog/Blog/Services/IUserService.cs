using Blog.DTO;
using Blog.Models;

namespace Blog.Services;

public interface IUserService
{
    Task<List<User>> GetUsersAsync();
    
    Task<User?> GetUserByIdAsync(int id);
    
    Task<User?> GetUserByLoginAsync(string login);
    
    Task<User> CreateUserAsync(RegisterUserRequest user);
    
    Task<AuthUserDto> AuthUserAsync(LoginUserRequest user);
    
    Task<int?> AuthorizationUserAsync(string token); 
}