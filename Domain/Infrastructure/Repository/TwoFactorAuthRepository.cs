using Microsoft.EntityFrameworkCore;
using WeCare.Domain.Infrastructure.Db;
using WeCare.Domain.Infrastructure.Entity;

namespace WeCare.Domain.Infrastructure.Repository;

public class TwoFactorAuthRepository : GenericRepository<TwoFactorAuth>, ITwoFactorAuthRepository
{
    public TwoFactorAuthRepository(WeCareDbContext context) : base(context) { }

    public async Task<TwoFactorAuth?> GetByUserIdAsync(Guid userId)
    {
        return await _dbSet.FirstOrDefaultAsync(t => t.UserId == userId);
    }
} 