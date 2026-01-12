using Blog.DTO;
using Blog.Models;

namespace Blog.Helpers;

public static class RecordMapper
{
    public static Record ToModel(CreateRecordRequest dto)
    {
        return new Record()
        {
            UserId = dto.UserId,
            Title = dto.Title,
            Content = dto.Content
        };
    }
    
    public static Record ToModel(UpdateRecordRequest dto)
    {
        return new Record()
        {
            Id = dto.RecordId,
            UserId = dto.UserId,
            Title = dto.Title,
            Content = dto.Content
        };
    }
}