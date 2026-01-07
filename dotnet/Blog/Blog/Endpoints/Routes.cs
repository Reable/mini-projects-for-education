using Blog.Routes;

namespace Blog.Endpoints;

public abstract class Routes
{
    public static void Configure(WebApplication app)
    {
        app.MapIndexRoutes();
        app.MapUserRoutes();
        
        app.MapFallback(() => Results.NotFound("Страница не найдена"));
    }
}