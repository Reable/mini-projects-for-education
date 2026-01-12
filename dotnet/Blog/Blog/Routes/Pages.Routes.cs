namespace Blog.Routes;

public static class PagesRoutes
{
    public static void MapPagesRoutes(this IEndpointRouteBuilder routes)
    {
        var route = routes.MapGroup("/");

        route.MapGet("/", async context =>
        {
            await RenderPage(context, "index");
        });
        
        route.MapGet("/auth", async context =>
        {
            await RenderPage(context, "auth");
        });
        
        route.MapGet("/dashboard", async context =>
        {
            await RenderPage(context, "dashboard");
        });
        
    }

    private static async Task RenderPage(HttpContext context, string file)
    {
        context.Response.ContentType = "text/html";
        await context.Response.SendFileAsync($"./Views/{file}.html");
    }
}