using MVC.Models;

namespace MVC.Services;

public class UserService
{
    private readonly List<User> _users;
    private int _lastId = 0;

    public UserService()
    {
        _users = [];
        GetDataFormSource();
    }

    private void GetDataFormSource()
    {
        _users.Add(new User() {Id = GetCurrentId(), Login =  "admin", Password = "admin password"});
        _users.Add(new User() {Id = GetCurrentId(), Login =  "admin 2", Password = "admin password 2"});
    }

    private int GetCurrentId() => ++_lastId;

    public List<User> GetAll() => _users;
    
    public User? GetById(int id) => _users.FirstOrDefault(x => x.Id == id);
    
    public User AddUser(User user)
    {
        user.Id = GetCurrentId();
        _users.Add(user);
        return user;
    }

    public bool Update(User user)
    {
        var found = GetById(user.Id);

        if (found == null)
            return false;

        found.Login = user.Login;
        found.Password = user.Password;
        
        return true;
    }
    
    public bool Delete(int id)
    {
        var found = GetById(id);
        if (found == null)
            return false;

        _users.Remove(found);
        
        return true;
    }
    
}