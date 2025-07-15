using System.ComponentModel.DataAnnotations;

namespace WeCare.Domain.Infrastructure.Entity;

public class Vaccine
{
    [Key]
    public Guid Id { get; set; }
    
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    [StringLength(500)]
    public string Description { get; set; } = string.Empty;
    
    [Required]
    public int AgeInWeeks { get; set; }
    
    [Required]
    public int AgeInMonths { get; set; }
    
    [StringLength(100)]
    public string? Manufacturer { get; set; }
    
    [StringLength(500)]
    public string? SideEffects { get; set; }
    
    [StringLength(500)]
    public string? Contraindications { get; set; }
    
    public bool IsRequired { get; set; } = true;
    
    public bool IsActive { get; set; } = true;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime? UpdatedAt { get; set; }
    
    // Navigation properties
    public virtual ICollection<VaccinationRecord> VaccinationRecords { get; set; } = new List<VaccinationRecord>();
} 