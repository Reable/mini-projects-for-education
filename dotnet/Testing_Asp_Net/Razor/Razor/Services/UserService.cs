using Razor.Models;

namespace Razor.Services;

public class UserService
{
    private List<User> _users;
    private int _lastId;

    public UserService()
    {
        _users = new();
        StartUo();
    }

    public void StartUo()
    {
        GetDataFromSource();
    }

    public void GetDataFromSource()
    {
        _users.Add(new User() {Id = GetCurrentNextId(), Login = "Login1",  Password = "Password1"});
        _users.Add(new User() {Id = GetCurrentNextId(), Login = "Login2",  Password = "Password2"});
        _users.Add(new User() {Id = GetCurrentNextId(), Login = "Login3",  Password = "Password3"});
    }

    private int GetCurrentNextId() => _lastId++;

    public List<User> GetAll() => _users;
    
    public User? GetById(int id) => _users.FirstOrDefault(u => u.Id == id) ?? null;

    public User Add(User user)
    {
        user.Id = GetCurrentNextId();
        _users.Add(user);
        return user;
    }

    public bool Update(User user)
    {
        var foundUser = GetById(user.Id);
        
        if(foundUser == null) 
            return false;
        
        foundUser.Login = user.Login;
        foundUser.Password = user.Password;
        
        return true;
    }

    public bool DeleteById(int id)
    {
        var foundUser = GetById(id);

        if (foundUser == null)
            return false;
        
        _users.Remove(foundUser);
        return true;
    }
}