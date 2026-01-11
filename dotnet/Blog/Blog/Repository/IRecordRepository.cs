using Blog.Models;

namespace Blog.Repository;

public interface IRecordRepository
{
    Task<List<Record>> GetAllAsync();

    Task<List<Record>> GetAllByUserIdAsync(int userId);
    
    Task<Record?> GetByIdAsync(int id);
    
    Task<Record> AddAsync(Record record);
    
    Task<Record> UpdateAsync(Record record);
    
    Task<bool> DeleteAsync(int id);
}