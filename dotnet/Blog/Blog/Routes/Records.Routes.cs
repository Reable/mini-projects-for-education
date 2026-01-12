using Blog.Controllers;

namespace Blog.Routes;

public static class RecordsRoutes
{
    public static void MapRecordsRoutes(this IEndpointRouteBuilder routes)
    {
        var route = routes.MapGroup("/records");
        
        route.MapGet("", RecordController.GetRecords);
        route.MapGet("{id:int}", RecordController.GetRecordById);
        
        route.MapGet("user/{userId:int}", RecordController.GetUserRecords);
        
        route.MapPost("/create", RecordController.CreateRecord);
        route.MapPut("/update", RecordController.UpdateRecord);
        route.MapDelete("/delete", RecordController.DeleteRecord);
    }
}