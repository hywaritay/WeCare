using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WeCare.Domain.Core.Dto;
using WeCare.Domain.Core.Service;
using WeCare.Domain.Utils;

namespace WeCare.Domain.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VaccinationController : ControllerBase
{
    private readonly IVaccinationService _vaccinationService;

    public VaccinationController(IVaccinationService vaccinationService)
    {
        _vaccinationService = vaccinationService;
    }

    [HttpPost]
    [Authorize(Roles = "CommunityMother,AdminDoctor")]
    public async Task<IActionResult> AddVaccinationRecord([FromBody] VaccinationRecordRequestDto dto)
    {
        var result = await _vaccinationService.AddVaccinationRecordAsync(dto);
        return Ok(new ApiResponse<VaccinationRecordResponseDto>(result));
    }

    [HttpPut("{recordId}")]
    [Authorize(Roles = "CommunityMother,AdminDoctor")]
    public async Task<IActionResult> UpdateVaccinationRecord(string recordId, [FromBody] VaccinationRecordRequestDto dto)
    {
        var result = await _vaccinationService.UpdateVaccinationRecordAsync(recordId, dto);
        return Ok(new ApiResponse<VaccinationRecordResponseDto>(result));
    }

    [HttpDelete("{recordId}")]
    [Authorize(Roles = "CommunityMother,AdminDoctor")]
    public async Task<IActionResult> DeleteVaccinationRecord(string recordId)
    {
        var result = await _vaccinationService.DeleteVaccinationRecordAsync(recordId);
        return Ok(new ApiResponse<bool>(result, true, "Vaccination record deleted successfully"));
    }

    [HttpGet("{recordId}")]
    [Authorize(Roles = "CommunityMother,AdminDoctor")]
    public async Task<IActionResult> GetVaccinationRecordById(string recordId)
    {
        var result = await _vaccinationService.GetVaccinationRecordByIdAsync(recordId);
        if (result == null) return NotFound(new ApiResponse<VaccinationRecordResponseDto>(null, false, "Vaccination record not found"));
        return Ok(new ApiResponse<VaccinationRecordResponseDto>(result));
    }

    [HttpGet("child/{childId}")]
    [Authorize(Roles = "CommunityMother,AdminDoctor")]
    public async Task<IActionResult> GetVaccinationRecordsByChildId(string childId)
    {
        var result = await _vaccinationService.GetVaccinationRecordsByChildIdAsync(childId);
        return Ok(new ApiResponse<IEnumerable<VaccinationRecordResponseDto>>(result));
    }

    [HttpGet("upcoming/{userId}")]
    [Authorize(Roles = "CommunityMother,AdminDoctor")]
    public async Task<IActionResult> GetUpcomingVaccinationsByUserId(string userId)
    {
        var result = await _vaccinationService.GetUpcomingVaccinationsByUserIdAsync(userId);
        return Ok(new ApiResponse<IEnumerable<VaccinationRecordResponseDto>>(result));
    }
} 