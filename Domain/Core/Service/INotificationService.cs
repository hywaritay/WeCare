using WeCare.Domain.Core.Dto;

namespace WeCare.Domain.Core.Service;

public interface INotificationService
{
    Task<NotificationResponseDto> AddNotificationAsync(NotificationRequestDto dto);
    Task<bool> MarkAsReadAsync(string notificationId);
    Task<bool> DeleteNotificationAsync(string notificationId);
    Task<NotificationResponseDto?> GetNotificationByIdAsync(string notificationId);
    Task<IEnumerable<NotificationResponseDto>> GetNotificationsByUserIdAsync(string userId);
    Task<IEnumerable<NotificationResponseDto>> GetUnreadNotificationsByUserIdAsync(string userId);
} 