using WeCare.Domain.Infrastructure.Entity;

namespace WeCare.Domain.Infrastructure.Repository;

public interface ITwoFactorAuthRepository : IGenericRepository<TwoFactorAuth>
{
    Task<TwoFactorAuth?> GetByUserIdAsync(Guid userId);
} 