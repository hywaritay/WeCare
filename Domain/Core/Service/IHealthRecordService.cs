using WeCare.Domain.Core.Dto;

namespace WeCare.Domain.Core.Service;

public interface IHealthRecordService
{
    Task<HealthRecordResponseDto> AddHealthRecordAsync(HealthRecordRequestDto dto);
    Task<HealthRecordResponseDto> UpdateHealthRecordAsync(string recordId, HealthRecordRequestDto dto);
    Task<bool> DeleteHealthRecordAsync(string recordId);
    Task<HealthRecordResponseDto?> GetHealthRecordByIdAsync(string recordId);
    Task<IEnumerable<HealthRecordResponseDto>> GetHealthRecordsByChildIdAsync(string childId);
} 