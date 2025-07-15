using Microsoft.EntityFrameworkCore;
using WeCare.Domain.Infrastructure.Db;
using WeCare.Domain.Infrastructure.Entity;

namespace WeCare.Domain.Infrastructure.Repository;

public class VaccinationRecordRepository : GenericRepository<VaccinationRecord>, IVaccinationRecordRepository
{
    public VaccinationRecordRepository(WeCareDbContext context) : base(context) { }

    public async Task<IEnumerable<VaccinationRecord>> GetByChildIdAsync(Guid childId)
    {
        return await _dbSet.Include(v => v.Vaccine).Where(v => v.ChildId == childId).ToListAsync();
    }

    public async Task<IEnumerable<VaccinationRecord>> GetUpcomingByUserIdAsync(Guid userId)
    {
        return await _dbSet
            .Include(v => v.Vaccine)
            .Include(v => v.Child)
            .Where(v => v.Child.MotherId == userId && v.ScheduledDate > DateTime.UtcNow && v.Status == VaccinationStatus.Scheduled)
            .ToListAsync();
    }
} 