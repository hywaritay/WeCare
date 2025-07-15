using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WeCare.Domain.Core.Dto;
using WeCare.Domain.Core.Service;
using WeCare.Domain.Utils;

namespace WeCare.Domain.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChildController : ControllerBase
{
    private readonly IChildService _childService;

    public ChildController(IChildService childService)
    {
        _childService = childService;
    }

    [HttpPost]
    [Authorize(Roles = "CommunityMother,AdminDoctor")]
    public async Task<IActionResult> RegisterChild([FromBody] ChildRequestDto dto)
    {
        var result = await _childService.RegisterChildAsync(dto);
        return Ok(new ApiResponse<ChildResponseDto>(result));
    }

    [HttpPut("{childId}")]
    [Authorize(Roles = "CommunityMother,AdminDoctor")]
    public async Task<IActionResult> UpdateChild(string childId, [FromBody] ChildRequestDto dto)
    {
        var result = await _childService.UpdateChildAsync(childId, dto);
        return Ok(new ApiResponse<ChildResponseDto>(result));
    }

    [HttpDelete("{childId}")]
    [Authorize(Roles = "CommunityMother,AdminDoctor")]
    public async Task<IActionResult> DeleteChild(string childId)
    {
        var result = await _childService.DeleteChildAsync(childId);
        return Ok(new ApiResponse<bool>(result, true, "Child deleted successfully"));
    }

    [HttpGet("{childId}")]
    [Authorize(Roles = "CommunityMother,AdminDoctor")]
    public async Task<IActionResult> GetChildById(string childId)
    {
        var result = await _childService.GetChildByIdAsync(childId);
        if (result == null) return NotFound(new ApiResponse<ChildResponseDto>(null, false, "Child not found"));
        return Ok(new ApiResponse<ChildResponseDto>(result));
    }

    [HttpGet("mother/{motherId}")]
    [Authorize(Roles = "CommunityMother,AdminDoctor")]
    public async Task<IActionResult> GetChildrenByMotherId(string motherId)
    {
        var result = await _childService.GetChildrenByMotherIdAsync(motherId);
        return Ok(new ApiResponse<IEnumerable<ChildResponseDto>>(result));
    }

    [HttpGet("mother/{motherId}/paginated")]
    [Authorize(Roles = "CommunityMother,AdminDoctor")]
    public async Task<IActionResult> GetChildrenByMotherIdPaginated(string motherId, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var result = await _childService.GetChildrenByMotherIdAsync(motherId);
        return Ok(new ApiResponse<IEnumerable<ChildResponseDto>>(result));
    }
} 