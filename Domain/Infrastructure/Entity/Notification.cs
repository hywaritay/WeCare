using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeCare.Domain.Infrastructure.Entity;

public class Notification
{
    [Key]
    public Guid Id { get; set; }
    
    [Required]
    public Guid UserId { get; set; }
    
    [Required]
    [StringLength(100)]
    public string Title { get; set; } = string.Empty;
    
    [Required]
    [StringLength(500)]
    public string Message { get; set; } = string.Empty;
    
    [Required]
    public NotificationType Type { get; set; }
    
    [Required]
    public NotificationChannel Channel { get; set; }
    
    public DateTime? ScheduledAt { get; set; }
    
    public DateTime? SentAt { get; set; }
    
    public DateTime? ReadAt { get; set; }
    
    public NotificationStatus Status { get; set; } = NotificationStatus.Pending;
    
    [StringLength(500)]
    public string? ErrorMessage { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime? UpdatedAt { get; set; }
    
    // Navigation properties
    [ForeignKey("UserId")]
    public virtual User User { get; set; } = null!;
}

public enum NotificationType
{
    VaccinationReminder = 1,
    AppointmentReminder = 2,
    HealthAlert = 3,
    EmergencyAlert = 4,
    GeneralInfo = 5
}

public enum NotificationChannel
{
    SMS = 1,
    Email = 2,
    PushNotification = 3,
    VoiceCall = 4
}

public enum NotificationStatus
{
    Pending = 1,
    Sent = 2,
    Delivered = 3,
    Failed = 4,
    Read = 5
} 