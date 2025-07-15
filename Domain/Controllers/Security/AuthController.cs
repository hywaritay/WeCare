using Microsoft.AspNetCore.Mvc;
using WeCare.Domain.Core.Dto;
using WeCare.Domain.Core.Service;
using WeCare.Domain.Utils;

namespace WeCare.Domain.Controllers.Security;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto dto)
    {
        var result = await _authService.RegisterAsync(dto);
        return Ok(new ApiResponse<LoginResponseDto>(result));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
    {
        var result = await _authService.LoginAsync(dto);
        return Ok(new ApiResponse<LoginResponseDto>(result));
    }

    [HttpPost("2fa/verify")]
    public async Task<IActionResult> Verify2FA([FromBody] TwoFactorRequestDto dto)
    {
        var result = await _authService.VerifyTwoFactorAsync(dto);
        if (!result.Success)
            return BadRequest(new ApiResponse<TwoFactorResponseDto>(result, false, result.Message));
        return Ok(new ApiResponse<TwoFactorResponseDto>(result));
    }

    [HttpPost("2fa/enable/{userId}")]
    public async Task<IActionResult> Enable2FA(string userId)
    {
        var result = await _authService.EnableTwoFactorAsync(userId);
        return Ok(new ApiResponse<bool>(result, true, "2FA enabled successfully"));
    }

    [HttpPost("2fa/disable/{userId}")]
    public async Task<IActionResult> Disable2FA(string userId)
    {
        var result = await _authService.DisableTwoFactorAsync(userId);
        return Ok(new ApiResponse<bool>(result, true, "2FA disabled successfully"));
    }
} 