using Blog.Routes;

namespace Blog.Endpoints;

public abstract class Routes
{
    public static void Configure(WebApplication app)
    {
        app.MapPagesRoutes();
        
        app.MapUserRoutes();
        app.MapRecordsRoutes();
        
        app.MapFallback(() => Results.NotFound("Страница не найдена"));
    }
}