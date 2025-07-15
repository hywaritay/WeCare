using WeCare.Domain.Infrastructure.Entity;

namespace WeCare.Domain.Infrastructure.Repository;

public interface IHealthRecordRepository : IGenericRepository<HealthRecord>
{
    Task<IEnumerable<HealthRecord>> GetByChildIdAsync(Guid childId);
} 