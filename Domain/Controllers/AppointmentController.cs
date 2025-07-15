using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WeCare.Domain.Core.Dto;
using WeCare.Domain.Core.Service;
using WeCare.Domain.Utils;

namespace WeCare.Domain.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AppointmentController : ControllerBase
{
    private readonly IAppointmentService _appointmentService;

    public AppointmentController(IAppointmentService appointmentService)
    {
        _appointmentService = appointmentService;
    }

    [HttpPost]
    [Authorize(Roles = "CommunityMother,AdminDoctor")]
    public async Task<IActionResult> AddAppointment([FromBody] AppointmentRequestDto dto)
    {
        var result = await _appointmentService.AddAppointmentAsync(dto);
        return Ok(new ApiResponse<AppointmentResponseDto>(result));
    }

    [HttpPut("{appointmentId}")]
    [Authorize(Roles = "CommunityMother,AdminDoctor")]
    public async Task<IActionResult> UpdateAppointment(string appointmentId, [FromBody] AppointmentRequestDto dto)
    {
        var result = await _appointmentService.UpdateAppointmentAsync(appointmentId, dto);
        return Ok(new ApiResponse<AppointmentResponseDto>(result));
    }

    [HttpDelete("{appointmentId}")]
    [Authorize(Roles = "CommunityMother,AdminDoctor")]
    public async Task<IActionResult> DeleteAppointment(string appointmentId)
    {
        var result = await _appointmentService.DeleteAppointmentAsync(appointmentId);
        return Ok(new ApiResponse<bool>(result, true, "Appointment deleted successfully"));
    }

    [HttpGet("{appointmentId}")]
    [Authorize(Roles = "CommunityMother,AdminDoctor")]
    public async Task<IActionResult> GetAppointmentById(string appointmentId)
    {
        var result = await _appointmentService.GetAppointmentByIdAsync(appointmentId);
        if (result == null) return NotFound(new ApiResponse<AppointmentResponseDto>(data: null, success: false, message: "Appointment not found"));
        return Ok(new ApiResponse<AppointmentResponseDto>(result));
    }

    [HttpGet("child/{childId}")]
    [Authorize(Roles = "CommunityMother,AdminDoctor")]
    public async Task<IActionResult> GetAppointmentsByChildId(string childId)
    {
        var result = await _appointmentService.GetAppointmentsByChildIdAsync(childId);
        return Ok(new ApiResponse<IEnumerable<AppointmentResponseDto>>(result));
    }

    [HttpGet("doctor/{doctorId}")]
    [Authorize(Roles = "AdminDoctor")]
    public async Task<IActionResult> GetAppointmentsByDoctorId(string doctorId)
    {
        var result = await _appointmentService.GetAppointmentsByDoctorIdAsync(doctorId);
        return Ok(new ApiResponse<IEnumerable<AppointmentResponseDto>>(result));
    }

    [HttpGet("doctor/{doctorId}/paginated")]
    [Authorize(Roles = "AdminDoctor")]
    public async Task<IActionResult> GetAppointmentsByDoctorIdPaginated(string doctorId, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var result = await _appointmentService.GetAppointmentsByDoctorIdPaginatedAsync(doctorId, page, pageSize);
        return Ok(new ApiResponse<Paginate<AppointmentResponseDto>>(result));
    }

    [HttpGet("upcoming/{userId}")]
    [Authorize(Roles = "CommunityMother,AdminDoctor")]
    public async Task<IActionResult> GetUpcomingAppointmentsByUserId(string userId)
    {
        var result = await _appointmentService.GetUpcomingAppointmentsByUserIdAsync(userId);
        return Ok(new ApiResponse<IEnumerable<AppointmentResponseDto>>(result));
    }
} 