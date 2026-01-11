using Blog.Models;
using Blog.Services;

namespace Blog.Repository;

public class RecordRepository : IRecordRepository
{
    
    private readonly List<Record> _records = [];
    private int _lastId = 0;

    public RecordRepository()
    {
        AutofillRecords();
    }

    private void AutofillRecords()
    {
        _records.Add(new Record(){Id = GetCurrentId(), UserId = 1, Title = "Title 1 1", Content = "Content 1 1"});
        _records.Add(new Record(){Id = GetCurrentId(), UserId = 1, Title = "Title 1 1", Content = "Content 1 2"});
        _records.Add(new Record(){Id = GetCurrentId(), UserId = 2, Title = "Title 2 1", Content = "Content 2 1"});
        _records.Add(new Record(){Id = GetCurrentId(), UserId = 2, Title = "Title 2 2", Content = "Content 2 2"});
        _records.Add(new Record(){Id = GetCurrentId(), UserId = 3, Title = "Title 3 1", Content = "Content 3 1"});
        _records.Add(new Record(){Id = GetCurrentId(), UserId = 3, Title = "Title 3 2", Content = "Content 3 2"});
    }
    
    public Task<List<Record>> GetAllAsync() => Task.FromResult(_records);
    
    public Task<List<Record>> GetAllByUserIdAsync(int userId)
    {
        var records = _records.FindAll(r => r.UserId == userId);
        return Task.FromResult(records);
    }

    public Task<Record?> GetByIdAsync(int id)
    {
        var record = _records.FirstOrDefault(r => r.Id == id);
        return Task.FromResult(record);
    }

    public Task<Record> AddAsync(Record record)
    {
        record.Id = GetCurrentId();
        _records.Add(record);
        return Task.FromResult(record);
    }

    public Task<Record> UpdateAsync(Record record)
    {
        var findRecord = _records.FirstOrDefault(r => r.Id == record.Id);
        if(findRecord == null)
            throw new Exception("Record not found");
        findRecord.Title = record.Title;
        findRecord.Content = record.Content;
        return Task.FromResult(findRecord);
    }

    public Task<bool> DeleteAsync(int id)
    {
        var findRecord = _records.FirstOrDefault(r => r.Id == id);
        if(findRecord == null)
            throw new Exception("Record not found");
        _records.Remove(findRecord);
        return Task.FromResult(true);
    }
    
    private int GetCurrentId() => ++_lastId;
}