using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WeCare.Domain.Core.Dto;
using WeCare.Domain.Core.Service;
using WeCare.Domain.Utils;

namespace WeCare.Domain.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HealthRecordController : ControllerBase
{
    private readonly IHealthRecordService _healthRecordService;

    public HealthRecordController(IHealthRecordService healthRecordService)
    {
        _healthRecordService = healthRecordService;
    }

    [HttpPost]
    [Authorize(Roles = "AdminDoctor")]
    public async Task<IActionResult> AddHealthRecord([FromBody] HealthRecordRequestDto dto)
    {
        var result = await _healthRecordService.AddHealthRecordAsync(dto);
        return Ok(new ApiResponse<HealthRecordResponseDto>(result));
    }

    [HttpPut("{recordId}")]
    [Authorize(Roles = "AdminDoctor")]
    public async Task<IActionResult> UpdateHealthRecord(string recordId, [FromBody] HealthRecordRequestDto dto)
    {
        var result = await _healthRecordService.UpdateHealthRecordAsync(recordId, dto);
        return Ok(new ApiResponse<HealthRecordResponseDto>(result));
    }

    [HttpDelete("{recordId}")]
    [Authorize(Roles = "AdminDoctor")]
    public async Task<IActionResult> DeleteHealthRecord(string recordId)
    {
        var result = await _healthRecordService.DeleteHealthRecordAsync(recordId);
        return Ok(new ApiResponse<bool>(result, true, "Health record deleted successfully"));
    }

    [HttpGet("{recordId}")]
    [Authorize(Roles = "CommunityMother,AdminDoctor")]
    public async Task<IActionResult> GetHealthRecordById(string recordId)
    {
        var result = await _healthRecordService.GetHealthRecordByIdAsync(recordId);
        if (result == null) return NotFound(new ApiResponse<HealthRecordResponseDto>(data: null, success: false, message: "Health record not found"));
        return Ok(new ApiResponse<HealthRecordResponseDto>(result));
    }

    [HttpGet("child/{childId}")]
    [Authorize(Roles = "CommunityMother,AdminDoctor")]
    public async Task<IActionResult> GetHealthRecordsByChildId(string childId)
    {
        var result = await _healthRecordService.GetHealthRecordsByChildIdAsync(childId);
        return Ok(new ApiResponse<IEnumerable<HealthRecordResponseDto>>(result));
    }
} 