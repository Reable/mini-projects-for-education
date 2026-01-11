using Blog.Models;

namespace Blog.Services;

public interface IRecordService
{
    Task<List<Record>> GetAllRecordsAsync();
    
    Task<Record?> GetRecordByIdAsync(int id);
    
    Task<Record> AddRecordAsync(Record record);
    
    Task<Record> UpdateRecordAsync(Record record);
    
    Task<bool> DeleteRecordAsync(int id);
}