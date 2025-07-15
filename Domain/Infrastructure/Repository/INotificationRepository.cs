using WeCare.Domain.Infrastructure.Entity;

namespace WeCare.Domain.Infrastructure.Repository;

public interface INotificationRepository : IGenericRepository<Notification>
{
    Task<IEnumerable<Notification>> GetByUserIdAsync(Guid userId);
    Task<IEnumerable<Notification>> GetUnreadByUserIdAsync(Guid userId);
} 