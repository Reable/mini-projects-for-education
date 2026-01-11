using Blog.Models;

namespace Blog.Repository;

public interface IUserRepository
{
    Task<List<User>> GetAllAsync();

    Task<User?> GetByIdAsync(int id);

    Task<User?> GetByLoginAsync(string login);

    Task<bool> ExistsByLoginAsync(string login);

    Task<User> AddAsync(User user);

}