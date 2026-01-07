using Blog.Models;

namespace Blog.Repository;

public class UserRepository : IUserRepository
{
    private readonly List<User> _users = [];

    public UserRepository()
    {
        AutofillUsers();
    }

    private void AutofillUsers()
    {
        _users.Add(new User(){Id = Guid.NewGuid().ToString(),  Login = "admin", Password = "admin"});
        _users.Add(new User(){Id = Guid.NewGuid().ToString(),  Login = "admin", Password = "admin"});
        _users.Add(new User(){Id = Guid.NewGuid().ToString(),  Login = "admin", Password = "admin"});
    }

    public Task<List<User>> GetAllAsync()
    {
        return Task.FromResult(_users);
    }

    public Task<User?> GetByIdAsync(string id)
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
        _users.Add(user);
        return Task.FromResult(user);
    }
}