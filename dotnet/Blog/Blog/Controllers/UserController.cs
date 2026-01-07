using Blog.DTO;
using Blog.Models;
using Blog.Services;

namespace Blog.Controllers;

public static class UserController
{
    public static async Task<IResult> GetUsers(IUserService userService)
    {
        try
        {
            var users = await userService.GetUsersAsync();
            var dto = users.Select(u => new UserDto(u.Id, u.Login)).ToList();
            return Results.Ok(dto);
        }
        catch (Exception e)
        {
            return Results.Problem(e.Message, "get users", 500);
        }
    }
}