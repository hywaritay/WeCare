using WeCare.Domain.Core.Dto;

namespace WeCare.Domain.Core.Service;

public interface IVaccinationService
{
    Task<VaccinationRecordResponseDto> AddVaccinationRecordAsync(VaccinationRecordRequestDto dto);
    Task<VaccinationRecordResponseDto> UpdateVaccinationRecordAsync(string recordId, VaccinationRecordRequestDto dto);
    Task<bool> DeleteVaccinationRecordAsync(string recordId);
    Task<VaccinationRecordResponseDto?> GetVaccinationRecordByIdAsync(string recordId);
    Task<IEnumerable<VaccinationRecordResponseDto>> GetVaccinationRecordsByChildIdAsync(string childId);
    Task<IEnumerable<VaccinationRecordResponseDto>> GetUpcomingVaccinationsByUserIdAsync(string userId);
} 