using WeCare.Domain.Core.Dto;
using WeCare.Domain.Infrastructure.Entity;
using WeCare.Domain.Infrastructure.Repository;

namespace WeCare.Domain.Core.Service;

public class VaccinationService : IVaccinationService
{
    private readonly IVaccinationRecordRepository _vaccinationRecordRepository;
    private readonly IChildRepository _childRepository;
    private readonly IUserRepository _userRepository;
    private readonly IGenericRepository<Vaccine> _vaccineRepository;

    public VaccinationService(
        IVaccinationRecordRepository vaccinationRecordRepository,
        IChildRepository childRepository,
        IUserRepository userRepository,
        IGenericRepository<Vaccine> vaccineRepository)
    {
        _vaccinationRecordRepository = vaccinationRecordRepository;
        _childRepository = childRepository;
        _userRepository = userRepository;
        _vaccineRepository = vaccineRepository;
    }

    public async Task<VaccinationRecordResponseDto> AddVaccinationRecordAsync(VaccinationRecordRequestDto dto)
    {
        if (!Guid.TryParse(dto.ChildId, out var childId))
            throw new Exception("Invalid child ID");
        if (!Guid.TryParse(dto.VaccineId, out var vaccineId))
            throw new Exception("Invalid vaccine ID");
        var child = await _childRepository.GetByIdAsync(childId);
        var vaccine = await _vaccineRepository.GetByIdAsync(vaccineId);
        if (child == null || vaccine == null)
            throw new Exception("Child or vaccine not found");
        var record = new VaccinationRecord
        {
            Id = Guid.NewGuid(),
            ChildId = childId,
            VaccineId = vaccineId,
            ScheduledDate = dto.ScheduledDate,
            AdministeredDate = dto.AdministeredDate,
            BatchNumber = dto.BatchNumber,
            AdministeredBy = dto.AdministeredBy,
            Notes = dto.Notes,
            Status = Enum.TryParse<VaccinationStatus>(dto.Status, out var s) ? s : VaccinationStatus.Scheduled,
            CreatedAt = DateTime.UtcNow
        };
        await _vaccinationRecordRepository.AddAsync(record);
        await _vaccinationRecordRepository.SaveChangesAsync();
        return ToResponseDto(record);
    }

    public async Task<VaccinationRecordResponseDto> UpdateVaccinationRecordAsync(string recordId, VaccinationRecordRequestDto dto)
    {
        if (!Guid.TryParse(recordId, out var id))
            throw new Exception("Invalid record ID");
        var record = await _vaccinationRecordRepository.GetByIdAsync(id);
        if (record == null)
            throw new Exception("Vaccination record not found");
        record.ScheduledDate = dto.ScheduledDate;
        record.AdministeredDate = dto.AdministeredDate;
        record.BatchNumber = dto.BatchNumber;
        record.AdministeredBy = dto.AdministeredBy;
        record.Notes = dto.Notes;
        record.Status = Enum.TryParse<VaccinationStatus>(dto.Status, out var s) ? s : record.Status;
        record.UpdatedAt = DateTime.UtcNow;
        _vaccinationRecordRepository.Update(record);
        await _vaccinationRecordRepository.SaveChangesAsync();
        return ToResponseDto(record);
    }

    public async Task<bool> DeleteVaccinationRecordAsync(string recordId)
    {
        if (!Guid.TryParse(recordId, out var id))
            return false;
        var record = await _vaccinationRecordRepository.GetByIdAsync(id);
        if (record == null)
            return false;
        _vaccinationRecordRepository.Remove(record);
        await _vaccinationRecordRepository.SaveChangesAsync();
        return true;
    }

    public async Task<VaccinationRecordResponseDto?> GetVaccinationRecordByIdAsync(string recordId)
    {
        if (!Guid.TryParse(recordId, out var id))
            return null;
        var record = await _vaccinationRecordRepository.GetByIdAsync(id);
        return record == null ? null : ToResponseDto(record);
    }

    public async Task<IEnumerable<VaccinationRecordResponseDto>> GetVaccinationRecordsByChildIdAsync(string childId)
    {
        if (!Guid.TryParse(childId, out var id))
            return Enumerable.Empty<VaccinationRecordResponseDto>();
        var records = await _vaccinationRecordRepository.GetByChildIdAsync(id);
        return records.Select(ToResponseDto);
    }

    public async Task<IEnumerable<VaccinationRecordResponseDto>> GetUpcomingVaccinationsByUserIdAsync(string userId)
    {
        if (!Guid.TryParse(userId, out var id))
            return Enumerable.Empty<VaccinationRecordResponseDto>();
        var records = await _vaccinationRecordRepository.GetUpcomingByUserIdAsync(id);
        return records.Select(ToResponseDto);
    }

    private VaccinationRecordResponseDto ToResponseDto(VaccinationRecord record)
    {
        return new VaccinationRecordResponseDto
        {
            Id = record.Id.ToString(),
            ChildId = record.ChildId.ToString(),
            VaccineId = record.VaccineId.ToString(),
            ScheduledDate = record.ScheduledDate,
            AdministeredDate = record.AdministeredDate,
            BatchNumber = record.BatchNumber,
            AdministeredBy = record.AdministeredBy,
            Notes = record.Notes,
            Status = record.Status.ToString()
        };
    }
} 