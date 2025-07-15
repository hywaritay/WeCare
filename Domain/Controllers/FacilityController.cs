using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WeCare.Domain.Core.Dto;
using WeCare.Domain.Core.Service;
using WeCare.Domain.Utils;

namespace WeCare.Domain.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FacilityController : ControllerBase
{
    private readonly IFacilityService _facilityService;

    public FacilityController(IFacilityService facilityService)
    {
        _facilityService = facilityService;
    }

    [HttpPost]
    [Authorize(Roles = "AdminDoctor")]
    public async Task<IActionResult> AddFacility([FromBody] FacilityRequestDto dto)
    {
        var result = await _facilityService.AddFacilityAsync(dto);
        return Ok(new ApiResponse<FacilityResponseDto>(result));
    }

    [HttpPut("{facilityId}")]
    [Authorize(Roles = "AdminDoctor")]
    public async Task<IActionResult> UpdateFacility(string facilityId, [FromBody] FacilityRequestDto dto)
    {
        var result = await _facilityService.UpdateFacilityAsync(facilityId, dto);
        return Ok(new ApiResponse<FacilityResponseDto>(result));
    }

    [HttpDelete("{facilityId}")]
    [Authorize(Roles = "AdminDoctor")]
    public async Task<IActionResult> DeleteFacility(string facilityId)
    {
        var result = await _facilityService.DeleteFacilityAsync(facilityId);
        return Ok(new ApiResponse<bool>(result, true, "Facility deleted successfully"));
    }

    [HttpGet("{facilityId}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetFacilityById(string facilityId)
    {
        var result = await _facilityService.GetFacilityByIdAsync(facilityId);
        if (result == null) return NotFound(new ApiResponse<FacilityResponseDto>(data: null, success: false, message: "Facility not found"));
        return Ok(new ApiResponse<FacilityResponseDto>(result));
    }

    [HttpGet("nearby")]
    [AllowAnonymous]
    public async Task<IActionResult> GetNearbyFacilities([FromQuery] double latitude, [FromQuery] double longitude, [FromQuery] double radiusKm)
    {
        var result = await _facilityService.GetNearbyFacilitiesAsync(latitude, longitude, radiusKm);
        return Ok(new ApiResponse<IEnumerable<FacilityResponseDto>>(result));
    }

    [HttpGet("type/{facilityType}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetFacilitiesByType(string facilityType)
    {
        var result = await _facilityService.GetFacilitiesByTypeAsync(facilityType);
        return Ok(new ApiResponse<IEnumerable<FacilityResponseDto>>(result));
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllFacilities()
    {
        var result = await _facilityService.GetAllFacilitiesAsync();
        return Ok(new ApiResponse<IEnumerable<FacilityResponseDto>>(result));
    }
} 