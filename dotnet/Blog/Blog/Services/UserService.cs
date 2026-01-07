using Blog.Exceptions;
using Blog.Models;
using Blog.Repository;
using BCryptNet = BCrypt.Net.BCrypt;

namespace Blog.Services;

public class UserService(IUserRepository userRepository) : IUserService
{
    private const string SaltSecret = "112233";

    public async Task<List<User>> GetUsersAsync() => await userRepository.GetAllAsync();

    public async Task<User?> GetUserAsync(string id) => await userRepository.GetByIdAsync(id);

    public async Task<User> CreateUserAsync(User user)
    {
        var find = await userRepository.GetByLoginAsync(user.Login);
        if (find != null)
            throw new UserExistsException(user.Login);

        user.Id = Guid.NewGuid().ToString();
        user.Password = HashPassword(user.Password);
        await userRepository.AddAsync(user);
        return user;
    }

    private static string HashPassword(string password) => BCryptNet.HashPassword(password, SaltSecret);
    private static bool ValidPassword(string hash) => BCryptNet.Verify(SaltSecret, hash);
    
}