using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeCare.Domain.Infrastructure.Entity;

public class HealthRecord
{
    [Key]
    public Guid Id { get; set; }
    
    [Required]
    public Guid ChildId { get; set; }
    
    [Required]
    public DateTime RecordDate { get; set; }
    
    [StringLength(100)]
    public string? RecordType { get; set; } // Checkup, Emergency, Follow-up, etc.
    
    [StringLength(500)]
    public string? Symptoms { get; set; }
    
    [StringLength(500)]
    public string? Diagnosis { get; set; }
    
    [StringLength(500)]
    public string? Treatment { get; set; }
    
    [StringLength(500)]
    public string? Prescription { get; set; }
    
    public double? Weight { get; set; } // in kg
    
    public double? Height { get; set; } // in cm
    
    public double? Temperature { get; set; } // in Celsius
    
    [StringLength(100)]
    public string? BloodPressure { get; set; }
    
    [StringLength(500)]
    public string? Notes { get; set; }
    
    [StringLength(100)]
    public string? DoctorName { get; set; }
    
    [StringLength(100)]
    public string? HospitalName { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime? UpdatedAt { get; set; }
    
    // Navigation properties
    [ForeignKey("ChildId")]
    public virtual Child Child { get; set; } = null!;
} 