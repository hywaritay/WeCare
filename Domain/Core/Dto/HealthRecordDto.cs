namespace WeCare.Domain.Core.Dto;

public class HealthRecordRequestDto
{
    public string ChildId { get; set; } = string.Empty;
    public DateTime RecordDate { get; set; }
    public string? RecordType { get; set; }
    public string? Symptoms { get; set; }
    public string? Diagnosis { get; set; }
    public string? Treatment { get; set; }
    public string? Prescription { get; set; }
    public double? Weight { get; set; }
    public double? Height { get; set; }
    public double? Temperature { get; set; }
    public string? BloodPressure { get; set; }
    public string? Notes { get; set; }
    public string? DoctorName { get; set; }
    public string? HospitalName { get; set; }
}

public class HealthRecordResponseDto
{
    public string Id { get; set; } = string.Empty;
    public string ChildId { get; set; } = string.Empty;
    public DateTime RecordDate { get; set; }
    public string? RecordType { get; set; }
    public string? Symptoms { get; set; }
    public string? Diagnosis { get; set; }
    public string? Treatment { get; set; }
    public string? Prescription { get; set; }
    public double? Weight { get; set; }
    public double? Height { get; set; }
    public double? Temperature { get; set; }
    public string? BloodPressure { get; set; }
    public string? Notes { get; set; }
    public string? DoctorName { get; set; }
    public string? HospitalName { get; set; }
} 