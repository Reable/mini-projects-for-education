using Blog.DTO;
using Blog.Models;

namespace Blog.Services;

public interface IRecordService
{
    Task<List<Record>> GetAllRecordsAsync();
    
    Task<List<Record>> GetAllUserRecordsAsync(int userId);
    
    Task<Record?> GetRecordByIdAsync(int id);
    
    Task<Record> AddRecordAsync(CreateRecordDto dto);
    
    Task<Record> UpdateRecordAsync(UpdateRecordDto dto);
    
    Task<bool> DeleteRecordAsync(DeleteRecordDto dto);
}