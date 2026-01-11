using Blog.DTO;
using Blog.Exceptions;
using Blog.Helpers;
using Blog.Models;
using Blog.Repository;

namespace Blog.Services;

public class RecordService(IUserService userService, IRecordRepository recordRepository) : IRecordService
{
    public async Task<List<Record>> GetAllRecordsAsync() => await recordRepository.GetAllAsync();

    public async Task<List<Record>> GetAllUserRecordsAsync(int userId) => await recordRepository.GetAllByUserIdAsync(userId);

    public async Task<Record?> GetRecordByIdAsync(int id) => await recordRepository.GetByIdAsync(id);

    public async Task<Record> AddRecordAsync(CreateRecordDto dto)
    {
        var findUser = await userService.GetUserByIdAsync(dto.UserId);
        
        if (findUser == null)
            throw new UsersNotExistsExceptions("User not found");
        
        var record = RecordMapper.ToModel(dto);
        
        return await recordRepository.AddAsync(record);
    }

    public async Task<Record> UpdateRecordAsync(UpdateRecordDto dto)
    {
        var findUser = await userService.GetUserByIdAsync(dto.UserId);
        
        if (findUser == null)
            throw new UsersNotExistsExceptions("User not found");
        
        var findRecord = await recordRepository.GetByIdAsync(dto.RecordId);
        if (findRecord == null)
            throw new Exception("Record not found");

        if (findRecord.UserId != dto.UserId)
            throw new Exception("Access denied");
        
        return await recordRepository.UpdateAsync(findRecord);
    }

    public async Task<bool> DeleteRecordAsync(DeleteRecordDto dto)
    {
        var findUser = await userService.GetUserByIdAsync(dto.UserId);
        
        if (findUser == null)
            throw new UsersNotExistsExceptions("User not found");
        
        var findRecord = await recordRepository.GetByIdAsync(dto.RecordId);
        if (findRecord == null)
            throw new Exception("Record not found");

        if (findRecord.UserId != dto.UserId)
            throw new Exception("Access denied");
        
        return await recordRepository.DeleteAsync(dto.RecordId);
    }
}