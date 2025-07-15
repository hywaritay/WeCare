using Microsoft.EntityFrameworkCore;
using WeCare.Domain.Infrastructure.Db;
using WeCare.Domain.Infrastructure.Entity;

namespace WeCare.Domain.Infrastructure.Repository;

public class NotificationRepository : GenericRepository<Notification>, INotificationRepository
{
    public NotificationRepository(WeCareDbContext context) : base(context) { }

    public async Task<IEnumerable<Notification>> GetByUserIdAsync(Guid userId)
    {
        return await _dbSet.Where(n => n.UserId == userId).ToListAsync();
    }

    public async Task<IEnumerable<Notification>> GetUnreadByUserIdAsync(Guid userId)
    {
        return await _dbSet.Where(n => n.UserId == userId && n.Status != NotificationStatus.Read).ToListAsync();
    }
} 