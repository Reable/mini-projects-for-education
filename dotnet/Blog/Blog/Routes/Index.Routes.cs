using Blog.Controllers;

namespace Blog.Routes;

public static class IndexRoutes
{
    public static void MapIndexRoutes(this IEndpointRouteBuilder routes)
    {
        var route = routes.MapGroup("/");
        
        route.MapGet("", RecordController.GetRecords);
        route.MapGet("{id:int}", RecordController.GetRecordById);
        
        route.MapGet("records/{userId:int}", RecordController.GetUserRecords);
    }
}