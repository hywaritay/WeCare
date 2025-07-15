using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeCare.Domain.Infrastructure.Entity;

public class VaccinationRecord
{
    [Key]
    public Guid Id { get; set; }
    
    [Required]
    public Guid ChildId { get; set; }
    
    [Required]
    public Guid VaccineId { get; set; }
    
    [Required]
    public DateTime ScheduledDate { get; set; }
    
    public DateTime? AdministeredDate { get; set; }
    
    [StringLength(100)]
    public string? BatchNumber { get; set; }
    
    [StringLength(100)]
    public string? AdministeredBy { get; set; }
    
    [StringLength(500)]
    public string? Notes { get; set; }
    
    public VaccinationStatus Status { get; set; } = VaccinationStatus.Scheduled;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime? UpdatedAt { get; set; }
    
    // Navigation properties
    [ForeignKey("ChildId")]
    public virtual Child Child { get; set; } = null!;
    
    [ForeignKey("VaccineId")]
    public virtual Vaccine Vaccine { get; set; } = null!;
}

public enum VaccinationStatus
{
    Scheduled = 1,
    Administered = 2,
    Missed = 3,
    Cancelled = 4
} 