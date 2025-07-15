namespace WeCare.Domain.Core.Dto;

public class ChildRequestDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string Gender { get; set; } = string.Empty;
    public string? BloodGroup { get; set; }
    public string? MedicalHistory { get; set; }
    public string? Allergies { get; set; }
    public double? Weight { get; set; }
    public double? Height { get; set; }
    public string MotherId { get; set; } = string.Empty;
}

public class ChildResponseDto
{
    public string Id { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string Gender { get; set; } = string.Empty;
    public string? BloodGroup { get; set; }
    public string? MedicalHistory { get; set; }
    public string? Allergies { get; set; }
    public double? Weight { get; set; }
    public double? Height { get; set; }
    public string MotherId { get; set; } = string.Empty;
    public bool IsActive { get; set; }
} 