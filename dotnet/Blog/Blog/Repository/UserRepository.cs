using Blog.Models;
using Blog.Helpers;

namespace Blog.Repository;

public class UserRepository : IUserRepository
{
    private readonly List<User> _users = [];
    private int _lastId = 0;

    public UserRepository()
    {
        AutofillUsers();
    }

    private void AutofillUsers()
    {
        _users.Add(new User(){Id = 1,  Login = "admin1", Password = CryptoHelper.HashPassword("admin12345")});
        _users.Add(new User(){Id = 2,  Login = "admin2", Password = CryptoHelper.HashPassword("admin23456")});
        _users.Add(new User(){Id = 3,  Login = "admin3", Password = CryptoHelper.HashPassword("admin34567")});
    }

    public Task<List<User>> GetAllAsync() => Task.FromResult(_users);

    public Task<User?> GetByIdAsync(int id)
    {
        var user = _users.FirstOrDefault(u => u.Id == id);
        return Task.FromResult(user);
    }

    public Task<User?> GetByLoginAsync(string login)
    {
        var user = _users.FirstOrDefault(u => u.Login == login);
        return Task.FromResult(user);
    }

    public Task<bool> ExistsByLoginAsync(string login)
    {
        var user = _users.FirstOrDefault(u => u.Login == login);
        return Task.FromResult(user != null);
    }

    public Task<User> AddAsync(User user)
    {
        user.Id = GetCurrentId();
        _users.Add(user);
        return Task.FromResult(user);
    }
    
    private int GetCurrentId() => ++_lastId;
}