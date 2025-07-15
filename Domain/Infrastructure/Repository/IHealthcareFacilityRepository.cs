using WeCare.Domain.Infrastructure.Entity;

namespace WeCare.Domain.Infrastructure.Repository;

public interface IHealthcareFacilityRepository : IGenericRepository<HealthcareFacility>
{
    Task<IEnumerable<HealthcareFacility>> GetNearbyAsync(double latitude, double longitude, double radiusKm);
    Task<IEnumerable<HealthcareFacility>> GetByTypeAsync(string facilityType);
} 