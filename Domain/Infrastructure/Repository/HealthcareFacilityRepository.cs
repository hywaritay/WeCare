using Microsoft.EntityFrameworkCore;
using WeCare.Domain.Infrastructure.Db;
using WeCare.Domain.Infrastructure.Entity;

namespace WeCare.Domain.Infrastructure.Repository;

public class HealthcareFacilityRepository : GenericRepository<HealthcareFacility>, IHealthcareFacilityRepository
{
    public HealthcareFacilityRepository(WeCareDbContext context) : base(context) { }

    public async Task<IEnumerable<HealthcareFacility>> GetNearbyAsync(double latitude, double longitude, double radiusKm)
    {
        var allFacilities = await _dbSet.ToListAsync();
        return allFacilities.Where(f => GetDistanceKm(latitude, longitude, f.Latitude, f.Longitude) <= radiusKm);
    }

    public async Task<IEnumerable<HealthcareFacility>> GetByTypeAsync(string facilityType)
    {
        return await _dbSet.Where(f => f.FacilityType == facilityType).ToListAsync();
    }

    private double GetDistanceKm(double lat1, double lon1, double lat2, double lon2)
    {
        double R = 6371; // Radius of the earth in km
        double dLat = ToRadians(lat2 - lat1);
        double dLon = ToRadians(lon2 - lon1);
        double a =
            Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
            Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) *
            Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
        double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        double distance = R * c;
        return distance;
    }

    private double ToRadians(double deg)
    {
        return deg * (Math.PI / 180);
    }
} 