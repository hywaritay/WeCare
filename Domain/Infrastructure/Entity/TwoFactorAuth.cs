using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeCare.Domain.Infrastructure.Entity;

public class TwoFactorAuth
{
    [Key]
    public Guid Id { get; set; }
    
    [Required]
    public Guid UserId { get; set; }
    
    [Required]
    [StringLength(100)]
    public string SecretKey { get; set; } = string.Empty;
    
    [Required]
    [StringLength(10)]
    public string BackupCode { get; set; } = string.Empty;
    
    public bool IsEnabled { get; set; } = false;
    
    public DateTime? LastUsedAt { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime? UpdatedAt { get; set; }
    
    // Navigation properties
    [ForeignKey("UserId")]
    public virtual User User { get; set; } = null!;

    public string? OtpHash { get; set; }
    public DateTime? OtpExpiresAt { get; set; }
} 