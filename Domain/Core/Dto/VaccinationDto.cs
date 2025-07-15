namespace WeCare.Domain.Core.Dto;

public class VaccinationRecordRequestDto
{
    public string ChildId { get; set; } = string.Empty;
    public string VaccineId { get; set; } = string.Empty;
    public DateTime ScheduledDate { get; set; }
    public DateTime? AdministeredDate { get; set; }
    public string? BatchNumber { get; set; }
    public string? AdministeredBy { get; set; }
    public string? Notes { get; set; }
    public string? Status { get; set; }
}

public class VaccinationRecordResponseDto
{
    public string Id { get; set; } = string.Empty;
    public string ChildId { get; set; } = string.Empty;
    public string VaccineId { get; set; } = string.Empty;
    public DateTime ScheduledDate { get; set; }
    public DateTime? AdministeredDate { get; set; }
    public string? BatchNumber { get; set; }
    public string? AdministeredBy { get; set; }
    public string? Notes { get; set; }
    public string Status { get; set; } = string.Empty;
} 