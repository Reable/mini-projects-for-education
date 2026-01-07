using Simple.Models;

namespace Simple.Controllers;

public class UsersController
{
    private List<User> _users;
    private int _currentId = 0;

    public UsersController()
    {
        _users = [];
        GetUsersFromSource();
    }

    private void GetUsersFromSource()
    {
        _users.Add(new User(){Id = GetCurrentId(), Login = "admin 1",  Password = "admin123"});
        _users.Add(new User(){Id = GetCurrentId(), Login = "admin 2",  Password = "12312333"});
        _users.Add(new User(){Id = GetCurrentId(), Login = "admin 3",  Password = "33333333"});
    }

    private int GetCurrentId() => ++_currentId;

    public async Task GetAllAsync(HttpResponse response)
    {
        await response.WriteAsJsonAsync(_users);
    }

    public async Task GetByIdAsync(HttpResponse response, int id)
    {
        var user = _users.FirstOrDefault(u => u.Id == id);
        if (user == null)
        {
            response.StatusCode = 404;
            await response.WriteAsJsonAsync(new
                {
                    message = "User not found"
                }
            );
        }

        await response.WriteAsJsonAsync(user);
    }

    public async Task CreateAsync(HttpRequest request, HttpResponse response)
    {
        try
        {
            var user = await request.ReadFromJsonAsync<User>();
            if (user == null )
                throw new Exception("Incorrect data");

            user.Id = GetCurrentId();
            _users.Add(user);
            await response.WriteAsJsonAsync(user);
        }
        catch (Exception e)
        {
            response.StatusCode = 400;
            await response.WriteAsJsonAsync(new
                {
                    message = e.Message
                }
            );
        }
    }

    public async Task UpdateAsync(HttpRequest request, HttpResponse response)
    {
        try
        {
            var user = await request.ReadFromJsonAsync<User>();
            if (user == null)
                throw new Exception("Incorrect data");
                
            var foundUser = _users.FirstOrDefault(u => u.Id == user.Id);
            if (foundUser == null)
                throw new Exception($"Not found user with id: {user.Id}");
            
            foundUser.Login = user.Login;
            foundUser.Password = user.Password;
            
            await response.WriteAsJsonAsync(user);
        }
        catch (Exception e)
        {
            response.StatusCode = 400;
            await response.WriteAsJsonAsync(new
                {
                    message = e.Message
                }
            );
        }
    }
    
    public async Task DeleteAsync(HttpResponse response, int? id)
    {
        try
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user == null)
                throw new Exception($"Not found user with id: {id}");
            
            _users.Remove(user);
            await response.WriteAsJsonAsync(user);
        }
        catch (Exception e)
        {
            response.StatusCode = 400;
            await response.WriteAsJsonAsync(new
                {
                    message = e.Message
                }
            );
        }
    }
}