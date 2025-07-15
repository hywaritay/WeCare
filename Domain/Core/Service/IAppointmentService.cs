using WeCare.Domain.Core.Dto;
using WeCare.Domain.Utils;

namespace WeCare.Domain.Core.Service;

public interface IAppointmentService
{
    Task<AppointmentResponseDto> AddAppointmentAsync(AppointmentRequestDto dto);
    Task<AppointmentResponseDto> UpdateAppointmentAsync(string appointmentId, AppointmentRequestDto dto);
    Task<bool> DeleteAppointmentAsync(string appointmentId);
    Task<AppointmentResponseDto?> GetAppointmentByIdAsync(string appointmentId);
    Task<IEnumerable<AppointmentResponseDto>> GetAppointmentsByChildIdAsync(string childId);
    Task<IEnumerable<AppointmentResponseDto>> GetAppointmentsByDoctorIdAsync(string doctorId);
    Task<IEnumerable<AppointmentResponseDto>> GetUpcomingAppointmentsByUserIdAsync(string userId);
    Task<Paginate<AppointmentResponseDto>> GetAppointmentsByDoctorIdPaginatedAsync(string doctorId, int page, int pageSize);
} 