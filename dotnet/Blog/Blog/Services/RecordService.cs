using Blog.Models;
using Blog.Repository;

namespace Blog.Services;

public class RecordService(IUserService userService, IRecordRepository recordRepository) : IRecordService
{
    public Task<List<Record>> GetAllRecordsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Record?> GetRecordByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Record> AddRecordAsync(Record record)
    {
        throw new NotImplementedException();
    }

    public Task<Record> UpdateRecordAsync(Record record)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteRecordAsync(int id)
    {
        throw new NotImplementedException();
    }
}