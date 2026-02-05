using Blog.Services;

namespace Blog.Endpoints;

public class AuthFilter(IUserService userService) : IEndpointFilter
{
    
    public async ValueTask<object?> InvokeAsync(
        EndpointFilterInvocationContext context,
        EndpointFilterDelegate next)
    {
        var httpContext = context.HttpContext;

        var authHeader = httpContext.Request.Headers["Authorization"].FirstOrDefault();

        if (string.IsNullOrEmpty(authHeader))
            return Results.Unauthorized();

        if (!authHeader.StartsWith("Bearer "))
            return Results.Unauthorized();

        var token = authHeader["Bearer ".Length..].Trim();

        var userId = await ValidateToken(token);
        
        if (userId == null)
            return Results.Problem("Invalid token");

        httpContext.Items["UserId"] = userId;

        return await next(context);
    }

    private async Task<int?> ValidateToken(string token)
    {
        var id = await userService.AuthorizationUserAsync(token);
        return id;
    }
}
