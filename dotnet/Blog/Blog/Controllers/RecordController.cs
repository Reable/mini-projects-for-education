using Blog.DTO;
using Blog.Exceptions;
using Blog.Models;
using Blog.Services;

namespace Blog.Controllers;

public static class RecordController
{
    public static async Task<IResult> GetRecords(IRecordService recordService)
    {
        try
        {
            var records = await recordService.GetAllRecordsAsync();
            var dto = records.Select(r => new RecordDto(r.Id, r.UserId, r.Title, r.Content)).ToList();            
            return Results.Ok(dto);
        }
        catch (Exception e)
        {
            return Results.Problem(e.Message, "", StatusCodes.Status500InternalServerError);
        }
    }

    public static async Task<IResult> GetRecordById(IRecordService recordService, int id)
    {
        try
        {
            var record = await recordService.GetRecordByIdAsync(id);
            return Results.Ok(record);                
        }
        catch (Exception e)
        {
            return Results.Problem(e.Message, "", StatusCodes.Status500InternalServerError);
        }
    }

    public static async Task<IResult> GetUserRecords(IRecordService recordService, IUserService userService, int userId)
    {
        try
        {
            var user = await userService.GetUserByIdAsync(userId);
            if(user == null)
                throw new UsersNotExistsExceptions("User not found");
            
            var records = await recordService.GetAllUserRecordsAsync(userId);
            return Results.Ok(records);
        }
        catch (Exception e)
        {
            return Results.Problem(e.Message, "", StatusCodes.Status400BadRequest);
        }
    }

    public static async Task<IResult> CreateRecord(CreateRecordRequest request, IRecordService recordService, IUserService userService)
    {
        try
        {
            var user = await userService.GetUserByIdAsync(request.UserId);
            if(user == null)
                throw new UsersNotExistsExceptions("User not found");

            var record = await recordService.AddRecordAsync(request);
            return Results.Ok(record);

        }
        catch (Exception e)
        {
            return Results.Problem(e.Message, "", StatusCodes.Status400BadRequest);
        }
    }
    
    public static async Task<IResult> UpdateRecord(IRecordService recordService, IUserService userService, UpdateRecordRequest request)
    {
        try
        {
            var user = await userService.GetUserByIdAsync(request.UserId);
            if(user == null)
                throw new UsersNotExistsExceptions("User not found");

            var record = await recordService.GetRecordByIdAsync(request.RecordId);
            if(record == null)
                throw new Exception("Record not found");
            if(record.UserId != request.UserId)
                throw new Exception("Access denied");
            
            await recordService.UpdateRecordAsync(request);
            
            return Results.Ok(record);

        }
        catch (Exception e)
        {
            return Results.Problem(e.Message, "", StatusCodes.Status400BadRequest);
        }
    }
    
    public static async Task<IResult> DeleteRecord(IRecordService recordService, IUserService userService)
    {
        return Results.BadRequest();
    }
}