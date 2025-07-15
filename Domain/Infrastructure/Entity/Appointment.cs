using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeCare.Domain.Infrastructure.Entity;

public class Appointment
{
    [Key]
    public Guid Id { get; set; }
    
    [Required]
    public Guid ChildId { get; set; }
    
    [Required]
    public Guid DoctorId { get; set; }
    
    [Required]
    public DateTime AppointmentDate { get; set; }
    
    [Required]
    public TimeSpan AppointmentTime { get; set; }
    
    [StringLength(100)]
    public string? AppointmentType { get; set; } // Vaccination, Checkup, Emergency, etc.
    
    [StringLength(500)]
    public string? Reason { get; set; }
    
    [StringLength(500)]
    public string? Notes { get; set; }
    
    public AppointmentStatus Status { get; set; } = AppointmentStatus.Scheduled;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime? UpdatedAt { get; set; }
    
    // Navigation properties
    [ForeignKey("ChildId")]
    public virtual Child Child { get; set; } = null!;
    
    [ForeignKey("DoctorId")]
    public virtual User Doctor { get; set; } = null!;
}

public enum AppointmentStatus
{
    Scheduled = 1,
    Confirmed = 2,
    Completed = 3,
    Cancelled = 4,
    NoShow = 5
} 