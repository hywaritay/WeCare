using WeCare.Domain.Core.Dto;
using WeCare.Domain.Infrastructure.Entity;
using WeCare.Domain.Infrastructure.Repository;
using WeCare.Domain.Utils;

namespace WeCare.Domain.Core.Service;

public class AppointmentService : IAppointmentService
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IChildRepository _childRepository;
    private readonly IUserRepository _userRepository;

    public AppointmentService(
        IAppointmentRepository appointmentRepository,
        IChildRepository childRepository,
        IUserRepository userRepository)
    {
        _appointmentRepository = appointmentRepository;
        _childRepository = childRepository;
        _userRepository = userRepository;
    }

    public async Task<AppointmentResponseDto> AddAppointmentAsync(AppointmentRequestDto dto)
    {
        if (!Guid.TryParse(dto.ChildId, out var childId))
            throw Except.BadRequest("Invalid child ID");
        if (!Guid.TryParse(dto.DoctorId, out var doctorId))
            throw Except.BadRequest("Invalid doctor ID");
        var child = await _childRepository.GetByIdAsync(childId);
        var doctor = await _userRepository.GetByIdAsync(doctorId);
        if (child == null || doctor == null)
            throw Except.NotFound("Child or doctor not found");
        var appointment = new Appointment
        {
            Id = Guid.NewGuid(),
            ChildId = childId,
            DoctorId = doctorId,
            AppointmentDate = dto.AppointmentDate,
            AppointmentTime = dto.AppointmentTime,
            AppointmentType = dto.AppointmentType,
            Reason = dto.Reason,
            Notes = dto.Notes,
            Status = Enum.TryParse<AppointmentStatus>(dto.Status, out var s) ? s : AppointmentStatus.Scheduled,
            CreatedAt = DateTime.UtcNow
        };
        await _appointmentRepository.AddAsync(appointment);
        await _appointmentRepository.SaveChangesAsync();
        return ToResponseDto(appointment);
    }

    public async Task<AppointmentResponseDto> UpdateAppointmentAsync(string appointmentId, AppointmentRequestDto dto)
    {
        if (!Guid.TryParse(appointmentId, out var id))
            throw Except.BadRequest("Invalid appointment ID");
        var appointment = await _appointmentRepository.GetByIdAsync(id);
        if (appointment == null)
            throw Except.NotFound("Appointment not found");
        appointment.AppointmentDate = dto.AppointmentDate;
        appointment.AppointmentTime = dto.AppointmentTime;
        appointment.AppointmentType = dto.AppointmentType;
        appointment.Reason = dto.Reason;
        appointment.Notes = dto.Notes;
        appointment.Status = Enum.TryParse<AppointmentStatus>(dto.Status, out var s) ? s : appointment.Status;
        appointment.UpdatedAt = DateTime.UtcNow;
        _appointmentRepository.Update(appointment);
        await _appointmentRepository.SaveChangesAsync();
        return ToResponseDto(appointment);
    }

    public async Task<bool> DeleteAppointmentAsync(string appointmentId)
    {
        if (!Guid.TryParse(appointmentId, out var id))
            throw Except.BadRequest("Invalid appointment ID");
        var appointment = await _appointmentRepository.GetByIdAsync(id);
        if (appointment == null)
            throw Except.NotFound("Appointment not found");
        _appointmentRepository.Remove(appointment);
        await _appointmentRepository.SaveChangesAsync();
        return true;
    }

    public async Task<AppointmentResponseDto?> GetAppointmentByIdAsync(string appointmentId)
    {
        if (!Guid.TryParse(appointmentId, out var id))
            throw Except.BadRequest("Invalid appointment ID");
        var appointment = await _appointmentRepository.GetByIdAsync(id);
        if (appointment == null)
            throw Except.NotFound("Appointment not found");
        return ToResponseDto(appointment);
    }

    public async Task<IEnumerable<AppointmentResponseDto>> GetAppointmentsByChildIdAsync(string childId)
    {
        if (!Guid.TryParse(childId, out var id))
            throw Except.BadRequest("Invalid child ID");
        var appointments = await _appointmentRepository.GetByChildIdAsync(id);
        return appointments.Select(ToResponseDto);
    }

    public async Task<IEnumerable<AppointmentResponseDto>> GetAppointmentsByDoctorIdAsync(string doctorId)
    {
        if (!Guid.TryParse(doctorId, out var id))
            throw Except.BadRequest("Invalid doctor ID");
        var appointments = await _appointmentRepository.GetByDoctorIdAsync(id);
        return appointments.Select(ToResponseDto);
    }

    public async Task<Paginate<AppointmentResponseDto>> GetAppointmentsByDoctorIdPaginatedAsync(string doctorId, int page, int pageSize)
    {
        if (!Guid.TryParse(doctorId, out var id))
            throw Except.BadRequest("Invalid doctor ID");
        var appointments = (await _appointmentRepository.GetByDoctorIdAsync(id)).ToList();
        var totalCount = appointments.Count;
        var items = appointments.Skip((page - 1) * pageSize).Take(pageSize).Select(ToResponseDto);
        return new Paginate<AppointmentResponseDto>(items, page, pageSize, totalCount);
    }

    public async Task<IEnumerable<AppointmentResponseDto>> GetUpcomingAppointmentsByUserIdAsync(string userId)
    {
        if (!Guid.TryParse(userId, out var id))
            throw Except.BadRequest("Invalid user ID");
        var appointments = await _appointmentRepository.GetUpcomingByUserIdAsync(id);
        return appointments.Select(ToResponseDto);
    }

    private AppointmentResponseDto ToResponseDto(Appointment appointment)
    {
        return new AppointmentResponseDto
        {
            Id = appointment.Id.ToString(),
            ChildId = appointment.ChildId.ToString(),
            DoctorId = appointment.DoctorId.ToString(),
            AppointmentDate = appointment.AppointmentDate,
            AppointmentTime = appointment.AppointmentTime,
            AppointmentType = appointment.AppointmentType,
            Reason = appointment.Reason,
            Notes = appointment.Notes,
            Status = appointment.Status.ToString()
        };
    }
} 