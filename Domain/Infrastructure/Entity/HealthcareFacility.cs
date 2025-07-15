using System.ComponentModel.DataAnnotations;

namespace WeCare.Domain.Infrastructure.Entity;

public class HealthcareFacility
{
    [Key]
    public Guid Id { get; set; }
    
    [Required]
    [StringLength(200)]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    [StringLength(500)]
    public string Address { get; set; } = string.Empty;
    
    [Required]
    public double Latitude { get; set; }
    
    [Required]
    public double Longitude { get; set; }
    
    [StringLength(20)]
    public string? PhoneNumber { get; set; }
    
    [EmailAddress]
    [StringLength(150)]
    public string? Email { get; set; }
    
    [StringLength(100)]
    public string? Website { get; set; }
    
    [StringLength(100)]
    public string FacilityType { get; set; } = string.Empty; // Hospital, Clinic, Health Center, etc.
    
    [StringLength(500)]
    public string? Services { get; set; } // Comma-separated list of services
    
    [StringLength(500)]
    public string? OperatingHours { get; set; }
    
    public bool IsEmergencyCenter { get; set; } = false;
    
    public bool IsVaccinationCenter { get; set; } = false;
    
    public bool IsActive { get; set; } = true;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime? UpdatedAt { get; set; }
} 