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
}