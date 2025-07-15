using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WeCare.Domain.Core.Dto;
using WeCare.Domain.Core.Service;
using WeCare.Domain.Utils;

namespace WeCare.Domain.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotificationController : ControllerBase
{
    private readonly INotificationService _notificationService;

    public NotificationController(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    [HttpPost]
    [Authorize(Roles = "CommunityMother,AdminDoctor")]
    public async Task<IActionResult> AddNotification([FromBody] NotificationRequestDto dto)
    {
        var result = await _notificationService.AddNotificationAsync(dto);
        return Ok(new ApiResponse<NotificationResponseDto>(result));
    }

    [HttpPut("read/{notificationId}")]
    [Authorize(Roles = "CommunityMother,AdminDoctor")]
    public async Task<IActionResult> MarkAsRead(string notificationId)
    {
        var result = await _notificationService.MarkAsReadAsync(notificationId);
        return Ok(new ApiResponse<bool>(result, true, "Notification marked as read"));
    }

    [HttpDelete("{notificationId}")]
    [Authorize(Roles = "CommunityMother,AdminDoctor")]
    public async Task<IActionResult> DeleteNotification(string notificationId)
    {
        var result = await _notificationService.DeleteNotificationAsync(notificationId);
        return Ok(new ApiResponse<bool>(result, true, "Notification deleted successfully"));
    }

    [HttpGet("{notificationId}")]
    [Authorize(Roles = "CommunityMother,AdminDoctor")]
    public async Task<IActionResult> GetNotificationById(string notificationId)
    {
        var result = await _notificationService.GetNotificationByIdAsync(notificationId);
        if (result == null) return NotFound(new ApiResponse<NotificationResponseDto>(data: null, success: false, message: "Notification not found"));
        return Ok(new ApiResponse<NotificationResponseDto>(result));
    }

    [HttpGet("user/{userId}")]
    [Authorize(Roles = "CommunityMother,AdminDoctor")]
    public async Task<IActionResult> GetNotificationsByUserId(string userId)
    {
        var result = await _notificationService.GetNotificationsByUserIdAsync(userId);
        return Ok(new ApiResponse<IEnumerable<NotificationResponseDto>>(result));
    }

    [HttpGet("user/{userId}/unread")]
    [Authorize(Roles = "CommunityMother,AdminDoctor")]
    public async Task<IActionResult> GetUnreadNotificationsByUserId(string userId)
    {
        var result = await _notificationService.GetUnreadNotificationsByUserIdAsync(userId);
        return Ok(new ApiResponse<IEnumerable<NotificationResponseDto>>(result));
    }
} 