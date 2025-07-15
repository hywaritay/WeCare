using Microsoft.EntityFrameworkCore;
using WeCare.Domain.Infrastructure.Db;
using WeCare.Domain.Infrastructure.Entity;

namespace WeCare.Domain.Infrastructure.Repository;

public class ChildRepository : GenericRepository<Child>, IChildRepository
{
    public ChildRepository(WeCareDbContext context) : base(context) { }

    public async Task<IEnumerable<Child>> GetByMotherIdAsync(Guid motherId)
    {
        return await _dbSet.Where(c => c.MotherId == motherId).ToListAsync();
    }
} 