using Blog.Controllers;
using Blog.Endpoints;

namespace Blog.Routes;

public static class RecordsRoutes
{
    public static void MapRecordsRoutes(this IEndpointRouteBuilder routes)
    {
        var route = routes.MapGroup("/records");
        
        route.MapGet("", RecordController.GetRecords);
        route.MapGet("{id:int}", RecordController.GetRecordById);
        
        route.MapGet("user/{userId:int}", RecordController.GetUserRecords);
        
        var securedRoute = route.MapGroup("")
            .AddEndpointFilter<AuthFilter>();

        securedRoute.MapPost("create", RecordController.CreateRecord);
        securedRoute.MapPut("update", RecordController.UpdateRecord);
        securedRoute.MapDelete("delete/{recordId:int}", RecordController.DeleteRecord);
    }
}