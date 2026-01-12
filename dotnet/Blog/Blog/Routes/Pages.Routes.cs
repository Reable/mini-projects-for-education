namespace Blog.Routes;

public static class PagesRoutes
{
    public static void MapPagesRoutes(this IEndpointRouteBuilder routes)
    {
        var route = routes.MapGroup("/");

        route.MapGet("/", async context => RenderPage(context, "index"));
    }

    private static async void RenderPage(HttpContext context, string file)
    {
        context.Response.ContentType = "text/html";
        await context.Response.SendFileAsync($"./Views/{file}.html");
    }
}