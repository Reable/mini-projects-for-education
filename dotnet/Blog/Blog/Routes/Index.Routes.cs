namespace Blog.Routes;

public static class IndexRoutes
{
    public static void MapIndexRoutes(this IEndpointRouteBuilder routes)
    {
        var route = routes.MapGroup("/");
        
        route.MapGet("", () => "Hello World!");
    }
}