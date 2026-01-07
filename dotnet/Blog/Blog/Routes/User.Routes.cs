using Blog.Controllers;
using Blog.DTO;
using Blog.Exceptions;
using Blog.Models;
using Blog.Services;

namespace Blog.Routes;

public static class UserRoutes
{
    public static void MapUserRoutes(this IEndpointRouteBuilder routes)
    {
        var route = routes.MapGroup("/user");

        route.MapGet("", UserController.GetUsers);
    }

}