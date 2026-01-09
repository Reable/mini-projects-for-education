using Blog.Controllers;

namespace Blog.Routes;

public static class UserRoutes
{
    public static void MapUserRoutes(this IEndpointRouteBuilder routes)
    {
        var route = routes.MapGroup("/users");

        route.MapGet("", UserController.GetUsers);
        route.MapGet("{id:int}", UserController.GetUser);
        
        route.MapPost("/register", UserController.CreateUser);
        route.MapPost("/login", UserController.AuthUser);
    }

}