namespace WeCare.Domain.Core.Dto;

public class FacilityRequestDto
{
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? Website { get; set; }
    public string FacilityType { get; set; } = string.Empty;
    public string? Services { get; set; }
    public string? OperatingHours { get; set; }
    public bool IsEmergencyCenter { get; set; }
    public bool IsVaccinationCenter { get; set; }
}

public class FacilityResponseDto
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? Website { get; set; }
    public string FacilityType { get; set; } = string.Empty;
    public string? Services { get; set; }
    public string? OperatingHours { get; set; }
    public bool IsEmergencyCenter { get; set; }
    public bool IsVaccinationCenter { get; set; }
    public bool IsActive { get; set; }
} 