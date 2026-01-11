using Blog.DTO;
using Blog.Exceptions;
using Blog.Helpers;
using Blog.Models;
using Blog.Repository;

namespace Blog.Services;

public class UserService(IUserRepository userRepository) : IUserService
{

    public async Task<List<User>> GetUsersAsync() => await userRepository.GetAllAsync();

    public async Task<User?> GetUserByIdAsync(int id) => await userRepository.GetByIdAsync(id);
   
    public async Task<User?> GetUserByLoginAsync(string login) => await userRepository.GetByLoginAsync(login);

    public async Task<User> CreateUserAsync(RegisterUserRequest userDto)
    {
        var find = await userRepository.GetByLoginAsync(userDto.Login);

        if (find != null)
            throw new UsersExistsExceptions(userDto.Login);

        var user = UserMapper.ToModel(userDto);
        user.Password = await CryptoHelper.HashPasswordAsync(user.Password);
        await userRepository.AddAsync(user);
    
        return user;
    }

    public async Task<string> AuthUserAsync(LoginUserRequest data)
    {
        var user = await userRepository.GetByLoginAsync(data.Login);
        if (user == null)
            throw new UserVerifyExceptions();
        
        var verify = CryptoHelper.VerifyPassword(data.Password, user.Password);
        if (!verify)
            throw new UserVerifyExceptions();
        
        return await JwtHelper.GenerateToken(user, TimeSpan.FromHours(12));
    }

    
    
}