using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeCare.Domain.Infrastructure.Entity;

public class Child
{
    [Key]
    public Guid Id { get; set; }
    
    [Required]
    [StringLength(100)]
    public string FirstName { get; set; } = string.Empty;
    
    [Required]
    [StringLength(100)]
    public string LastName { get; set; } = string.Empty;
    
    [Required]
    public DateTime DateOfBirth { get; set; }
    
    [Required]
    public Gender Gender { get; set; }
    
    [StringLength(20)]
    public string? BloodGroup { get; set; }
    
    [StringLength(500)]
    public string? MedicalHistory { get; set; }
    
    [StringLength(500)]
    public string? Allergies { get; set; }
    
    public double? Weight { get; set; } // in kg
    
    public double? Height { get; set; } // in cm
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime? UpdatedAt { get; set; }
    
    public bool IsActive { get; set; } = true;
    
    // Foreign key
    [Required]
    public Guid MotherId { get; set; }
    
    // Navigation properties
    [ForeignKey("MotherId")]
    public virtual User Mother { get; set; } = null!;
    
    public virtual ICollection<VaccinationRecord> VaccinationRecords { get; set; } = new List<VaccinationRecord>();
    public virtual ICollection<HealthRecord> HealthRecords { get; set; } = new List<HealthRecord>();
    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}

public enum Gender
{
    Male = 1,
    Female = 2,
    Other = 3
} 