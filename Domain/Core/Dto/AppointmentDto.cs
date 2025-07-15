namespace WeCare.Domain.Core.Dto;

public class AppointmentRequestDto
{
    public string ChildId { get; set; } = string.Empty;
    public string DoctorId { get; set; } = string.Empty;
    public DateTime AppointmentDate { get; set; }
    public TimeSpan AppointmentTime { get; set; }
    public string? AppointmentType { get; set; }
    public string? Reason { get; set; }
    public string? Notes { get; set; }
    public string? Status { get; set; }
}

public class AppointmentResponseDto
{
    public string Id { get; set; } = string.Empty;
    public string ChildId { get; set; } = string.Empty;
    public string DoctorId { get; set; } = string.Empty;
    public DateTime AppointmentDate { get; set; }
    public TimeSpan AppointmentTime { get; set; }
    public string? AppointmentType { get; set; }
    public string? Reason { get; set; }
    public string? Notes { get; set; }
    public string Status { get; set; } = string.Empty;
} 