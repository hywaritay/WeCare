using WeCare.Domain.Core.Dto;

namespace WeCare.Domain.Core.Service;

public interface IFacilityService
{
    Task<FacilityResponseDto> AddFacilityAsync(FacilityRequestDto dto);
    Task<FacilityResponseDto> UpdateFacilityAsync(string facilityId, FacilityRequestDto dto);
    Task<bool> DeleteFacilityAsync(string facilityId);
    Task<FacilityResponseDto?> GetFacilityByIdAsync(string facilityId);
    Task<IEnumerable<FacilityResponseDto>> GetNearbyFacilitiesAsync(double latitude, double longitude, double radiusKm);
    Task<IEnumerable<FacilityResponseDto>> GetFacilitiesByTypeAsync(string facilityType);
    Task<IEnumerable<FacilityResponseDto>> GetAllFacilitiesAsync();
} 