using Blog.DTO;
using Blog.Exceptions;
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
            return Results.Problem(e.Message, "", StatusCodes.Status500InternalServerError);
        }
    }

    public static async Task<IResult> GetUser(IUserService userService, int id)
    {
        try
        {
            var user = await userService.GetUserByIdAsync(id);
            if(user == null)
                throw new ArgumentException($"User with id {id} not found");
            
            var dto = new UserDto(user.Id, user.Login);
            return Results.Ok(dto);
        }
        catch (Exception e)
        {
            return Results.Problem(e.Message, "",  StatusCodes.Status400BadRequest);
        }
    }

    public static async Task<IResult> RegisterUser(IUserService userService, RegisterUserRequest data)
    {
        try
        {
            var user = await userService.CreateUserAsync(data);
            var dto = new UserDto(user.Id, user.Login);
            return Results.Created();
        }
        catch (Exception e)
        {
            return Results.Problem(e.Message, "", StatusCodes.Status409Conflict);
        }
    }

    public static async Task<IResult> AuthUser(IUserService userService, LoginUserRequest data)
    {
        try
        {
            var token = await userService.AuthUserAsync(data);
            return Results.Ok(token);
        }
        catch (Exception e)
        {
            return Results.Problem(e.Message, "", StatusCodes.Status401Unauthorized);
        }
    }

    public static async Task<IResult> AuthorizationUser(IUserService userService)
    {
        try
        {
            var token = await userService.AuthorizationUser(data);
            return Results.Ok(token);
        }
        catch (Exception e)
        {
            return Results.Problem(e.Message, "", StatusCodes.Status409Conflict);
        }
    }
}