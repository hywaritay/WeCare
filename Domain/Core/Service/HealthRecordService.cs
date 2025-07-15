using WeCare.Domain.Core.Dto;
using WeCare.Domain.Infrastructure.Entity;
using WeCare.Domain.Infrastructure.Repository;

namespace WeCare.Domain.Core.Service;

public class HealthRecordService : IHealthRecordService
{
    private readonly IHealthRecordRepository _healthRecordRepository;
    private readonly IChildRepository _childRepository;

    public HealthRecordService(IHealthRecordRepository healthRecordRepository, IChildRepository childRepository)
    {
        _healthRecordRepository = healthRecordRepository;
        _childRepository = childRepository;
    }

    public async Task<HealthRecordResponseDto> AddHealthRecordAsync(HealthRecordRequestDto dto)
    {
        if (!Guid.TryParse(dto.ChildId, out var childId))
            throw new Exception("Invalid child ID");
        var child = await _childRepository.GetByIdAsync(childId);
        if (child == null)
            throw new Exception("Child not found");
        var record = new HealthRecord
        {
            Id = Guid.NewGuid(),
            ChildId = childId,
            RecordDate = dto.RecordDate,
            RecordType = dto.RecordType,
            Symptoms = dto.Symptoms,
            Diagnosis = dto.Diagnosis,
            Treatment = dto.Treatment,
            Prescription = dto.Prescription,
            Weight = dto.Weight,
            Height = dto.Height,
            Temperature = dto.Temperature,
            BloodPressure = dto.BloodPressure,
            Notes = dto.Notes,
            DoctorName = dto.DoctorName,
            HospitalName = dto.HospitalName,
            CreatedAt = DateTime.UtcNow
        };
        await _healthRecordRepository.AddAsync(record);
        await _healthRecordRepository.SaveChangesAsync();
        return ToResponseDto(record);
    }

    public async Task<HealthRecordResponseDto> UpdateHealthRecordAsync(string recordId, HealthRecordRequestDto dto)
    {
        if (!Guid.TryParse(recordId, out var id))
            throw new Exception("Invalid record ID");
        var record = await _healthRecordRepository.GetByIdAsync(id);
        if (record == null)
            throw new Exception("Health record not found");
        record.RecordDate = dto.RecordDate;
        record.RecordType = dto.RecordType;
        record.Symptoms = dto.Symptoms;
        record.Diagnosis = dto.Diagnosis;
        record.Treatment = dto.Treatment;
        record.Prescription = dto.Prescription;
        record.Weight = dto.Weight;
        record.Height = dto.Height;
        record.Temperature = dto.Temperature;
        record.BloodPressure = dto.BloodPressure;
        record.Notes = dto.Notes;
        record.DoctorName = dto.DoctorName;
        record.HospitalName = dto.HospitalName;
        record.UpdatedAt = DateTime.UtcNow;
        _healthRecordRepository.Update(record);
        await _healthRecordRepository.SaveChangesAsync();
        return ToResponseDto(record);
    }

    public async Task<bool> DeleteHealthRecordAsync(string recordId)
    {
        if (!Guid.TryParse(recordId, out var id))
            return false;
        var record = await _healthRecordRepository.GetByIdAsync(id);
        if (record == null)
            return false;
        _healthRecordRepository.Remove(record);
        await _healthRecordRepository.SaveChangesAsync();
        return true;
    }

    public async Task<HealthRecordResponseDto?> GetHealthRecordByIdAsync(string recordId)
    {
        if (!Guid.TryParse(recordId, out var id))
            return null;
        var record = await _healthRecordRepository.GetByIdAsync(id);
        return record == null ? null : ToResponseDto(record);
    }

    public async Task<IEnumerable<HealthRecordResponseDto>> GetHealthRecordsByChildIdAsync(string childId)
    {
        if (!Guid.TryParse(childId, out var id))
            return Enumerable.Empty<HealthRecordResponseDto>();
        var records = await _healthRecordRepository.GetByChildIdAsync(id);
        return records.Select(ToResponseDto);
    }

    private HealthRecordResponseDto ToResponseDto(HealthRecord record)
    {
        return new HealthRecordResponseDto
        {
            Id = record.Id.ToString(),
            ChildId = record.ChildId.ToString(),
            RecordDate = record.RecordDate,
            RecordType = record.RecordType,
            Symptoms = record.Symptoms,
            Diagnosis = record.Diagnosis,
            Treatment = record.Treatment,
            Prescription = record.Prescription,
            Weight = record.Weight,
            Height = record.Height,
            Temperature = record.Temperature,
            BloodPressure = record.BloodPressure,
            Notes = record.Notes,
            DoctorName = record.DoctorName,
            HospitalName = record.HospitalName
        };
    }
} 