using WeCare.Domain.Core.Dto;
using WeCare.Domain.Infrastructure.Entity;
using WeCare.Domain.Infrastructure.Repository;

namespace WeCare.Domain.Core.Service;

public class NotificationService : INotificationService
{
    private readonly INotificationRepository _notificationRepository;
    private readonly IUserRepository _userRepository;

    public NotificationService(INotificationRepository notificationRepository, IUserRepository userRepository)
    {
        _notificationRepository = notificationRepository;
        _userRepository = userRepository;
    }

    public async Task<NotificationResponseDto> AddNotificationAsync(NotificationRequestDto dto)
    {
        if (!Guid.TryParse(dto.UserId, out var userId))
            throw new Exception("Invalid user ID");
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
            throw new Exception("User not found");
        var notification = new Notification
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Title = dto.Title,
            Message = dto.Message,
            Type = Enum.TryParse<NotificationType>(dto.Type, out var t) ? t : NotificationType.GeneralInfo,
            Channel = Enum.TryParse<NotificationChannel>(dto.Channel, out var c) ? c : NotificationChannel.SMS,
            ScheduledAt = dto.ScheduledAt,
            Status = NotificationStatus.Pending,
            CreatedAt = DateTime.UtcNow
        };
        await _notificationRepository.AddAsync(notification);
        await _notificationRepository.SaveChangesAsync();
        return ToResponseDto(notification);
    }

    public async Task<bool> MarkAsReadAsync(string notificationId)
    {
        if (!Guid.TryParse(notificationId, out var id))
            return false;
        var notification = await _notificationRepository.GetByIdAsync(id);
        if (notification == null)
            return false;
        notification.Status = NotificationStatus.Read;
        notification.ReadAt = DateTime.UtcNow;
        _notificationRepository.Update(notification);
        await _notificationRepository.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteNotificationAsync(string notificationId)
    {
        if (!Guid.TryParse(notificationId, out var id))
            return false;
        var notification = await _notificationRepository.GetByIdAsync(id);
        if (notification == null)
            return false;
        _notificationRepository.Remove(notification);
        await _notificationRepository.SaveChangesAsync();
        return true;
    }

    public async Task<NotificationResponseDto?> GetNotificationByIdAsync(string notificationId)
    {
        if (!Guid.TryParse(notificationId, out var id))
            return null;
        var notification = await _notificationRepository.GetByIdAsync(id);
        return notification == null ? null : ToResponseDto(notification);
    }

    public async Task<IEnumerable<NotificationResponseDto>> GetNotificationsByUserIdAsync(string userId)
    {
        if (!Guid.TryParse(userId, out var id))
            return Enumerable.Empty<NotificationResponseDto>();
        var notifications = await _notificationRepository.GetByUserIdAsync(id);
        return notifications.Select(ToResponseDto);
    }

    public async Task<IEnumerable<NotificationResponseDto>> GetUnreadNotificationsByUserIdAsync(string userId)
    {
        if (!Guid.TryParse(userId, out var id))
            return Enumerable.Empty<NotificationResponseDto>();
        var notifications = await _notificationRepository.GetUnreadByUserIdAsync(id);
        return notifications.Select(ToResponseDto);
    }

    private NotificationResponseDto ToResponseDto(Notification notification)
    {
        return new NotificationResponseDto
        {
            Id = notification.Id.ToString(),
            UserId = notification.UserId.ToString(),
            Title = notification.Title,
            Message = notification.Message,
            Type = notification.Type.ToString(),
            Channel = notification.Channel.ToString(),
            ScheduledAt = notification.ScheduledAt,
            SentAt = notification.SentAt,
            ReadAt = notification.ReadAt,
            Status = notification.Status.ToString(),
            ErrorMessage = notification.ErrorMessage
        };
    }
} 