using Microsoft.EntityFrameworkCore;
using WeCare.Domain.Infrastructure.Db;
using WeCare.Domain.Infrastructure.Entity;

namespace WeCare.Domain.Infrastructure.Repository;

public class HealthRecordRepository : GenericRepository<HealthRecord>, IHealthRecordRepository
{
    public HealthRecordRepository(WeCareDbContext context) : base(context) { }

    public async Task<IEnumerable<HealthRecord>> GetByChildIdAsync(Guid childId)
    {
        return await _dbSet.Where(h => h.ChildId == childId).ToListAsync();
    }
} 