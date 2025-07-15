using WeCare.Domain.Infrastructure.Entity;

namespace WeCare.Domain.Infrastructure.Repository;

public interface IVaccinationRecordRepository : IGenericRepository<VaccinationRecord>
{
    Task<IEnumerable<VaccinationRecord>> GetByChildIdAsync(Guid childId);
    Task<IEnumerable<VaccinationRecord>> GetUpcomingByUserIdAsync(Guid userId);
} 