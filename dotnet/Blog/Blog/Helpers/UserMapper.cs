using Blog.DTO;
using Blog.Models;

namespace Blog.Helpers;

public static class UserMapper
{
    public static User ToModel(RegisterUserRequest dto)
    {
        return new User()
        {
            Login = dto.Login,
            Password = dto.Password
        };
    }
}