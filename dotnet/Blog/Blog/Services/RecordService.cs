using Blog.DTO;
using Blog.Exceptions;
using Blog.Helpers;
using Blog.Models;
using Blog.Repository;

namespace Blog.Services;

public class RecordService(IRecordRepository recordRepository) : IRecordService
{
    public async Task<List<Record>> GetAllRecordsAsync() => await recordRepository.GetAllAsync();

    public async Task<List<Record>> GetAllUserRecordsAsync(int userId) => await recordRepository.GetAllByUserIdAsync(userId);

    public async Task<Record?> GetRecordByIdAsync(int id) => await recordRepository.GetByIdAsync(id);

    public async Task<Record> AddRecordAsync(CreateRecordRequest dto)
    {
        var record = RecordMapper.ToModel(dto);
        return await recordRepository.AddAsync(record);
    }

    public async Task<Record> UpdateRecordAsync(UpdateRecordRequest dto)
    {
        var findRecord = await recordRepository.GetByIdAsync(dto.RecordId);
        if (findRecord == null)
            throw new RecordsNotExistsExceptions();

        if (findRecord.UserId != dto.UserId)
            throw new NotAccessToUpdateRecordExceptions();

        var record = RecordMapper.ToModel(dto);
        return await recordRepository.UpdateAsync(record);
    }

    public async Task<bool> DeleteRecordAsync(DeleteRecordRequest dto)
    {
        var findRecord = await recordRepository.GetByIdAsync(dto.RecordId);
        if (findRecord == null)
            throw new RecordsNotExistsExceptions();

        if (findRecord.UserId != dto.UserId)
            throw new NotAccessToUpdateRecordExceptions();
        
        return await recordRepository.DeleteAsync(dto.RecordId);
    }
}