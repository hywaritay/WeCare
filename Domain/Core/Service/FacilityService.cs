using WeCare.Domain.Core.Dto;
using WeCare.Domain.Infrastructure.Entity;
using WeCare.Domain.Infrastructure.Repository;

namespace WeCare.Domain.Core.Service;

public class FacilityService : IFacilityService
{
    private readonly IHealthcareFacilityRepository _facilityRepository;

    public FacilityService(IHealthcareFacilityRepository facilityRepository)
    {
        _facilityRepository = facilityRepository;
    }

    public async Task<FacilityResponseDto> AddFacilityAsync(FacilityRequestDto dto)
    {
        var facility = new HealthcareFacility
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Address = dto.Address,
            Latitude = dto.Latitude,
            Longitude = dto.Longitude,
            PhoneNumber = dto.PhoneNumber,
            Email = dto.Email,
            Website = dto.Website,
            FacilityType = dto.FacilityType,
            Services = dto.Services,
            OperatingHours = dto.OperatingHours,
            IsEmergencyCenter = dto.IsEmergencyCenter,
            IsVaccinationCenter = dto.IsVaccinationCenter,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };
        await _facilityRepository.AddAsync(facility);
        await _facilityRepository.SaveChangesAsync();
        return ToResponseDto(facility);
    }

    public async Task<FacilityResponseDto> UpdateFacilityAsync(string facilityId, FacilityRequestDto dto)
    {
        if (!Guid.TryParse(facilityId, out var id))
            throw new Exception("Invalid facility ID");
        var facility = await _facilityRepository.GetByIdAsync(id);
        if (facility == null)
            throw new Exception("Facility not found");
        facility.Name = dto.Name;
        facility.Address = dto.Address;
        facility.Latitude = dto.Latitude;
        facility.Longitude = dto.Longitude;
        facility.PhoneNumber = dto.PhoneNumber;
        facility.Email = dto.Email;
        facility.Website = dto.Website;
        facility.FacilityType = dto.FacilityType;
        facility.Services = dto.Services;
        facility.OperatingHours = dto.OperatingHours;
        facility.IsEmergencyCenter = dto.IsEmergencyCenter;
        facility.IsVaccinationCenter = dto.IsVaccinationCenter;
        facility.UpdatedAt = DateTime.UtcNow;
        _facilityRepository.Update(facility);
        await _facilityRepository.SaveChangesAsync();
        return ToResponseDto(facility);
    }

    public async Task<bool> DeleteFacilityAsync(string facilityId)
    {
        if (!Guid.TryParse(facilityId, out var id))
            return false;
        var facility = await _facilityRepository.GetByIdAsync(id);
        if (facility == null)
            return false;
        _facilityRepository.Remove(facility);
        await _facilityRepository.SaveChangesAsync();
        return true;
    }

    public async Task<FacilityResponseDto?> GetFacilityByIdAsync(string facilityId)
    {
        if (!Guid.TryParse(facilityId, out var id))
            return null;
        var facility = await _facilityRepository.GetByIdAsync(id);
        return facility == null ? null : ToResponseDto(facility);
    }

    public async Task<IEnumerable<FacilityResponseDto>> GetNearbyFacilitiesAsync(double latitude, double longitude, double radiusKm)
    {
        var facilities = await _facilityRepository.GetNearbyAsync(latitude, longitude, radiusKm);
        return facilities.Select(ToResponseDto);
    }

    public async Task<IEnumerable<FacilityResponseDto>> GetFacilitiesByTypeAsync(string facilityType)
    {
        var facilities = await _facilityRepository.GetByTypeAsync(facilityType);
        return facilities.Select(ToResponseDto);
    }

    public async Task<IEnumerable<FacilityResponseDto>> GetAllFacilitiesAsync()
    {
        var facilities = await _facilityRepository.GetAllAsync();
        return facilities.Select(ToResponseDto);
    }

    private FacilityResponseDto ToResponseDto(HealthcareFacility facility)
    {
        return new FacilityResponseDto
        {
            Id = facility.Id.ToString(),
            Name = facility.Name,
            Address = facility.Address,
            Latitude = facility.Latitude,
            Longitude = facility.Longitude,
            PhoneNumber = facility.PhoneNumber,
            Email = facility.Email,
            Website = facility.Website,
            FacilityType = facility.FacilityType,
            Services = facility.Services,
            OperatingHours = facility.OperatingHours,
            IsEmergencyCenter = facility.IsEmergencyCenter,
            IsVaccinationCenter = facility.IsVaccinationCenter,
            IsActive = facility.IsActive
        };
    }
} 