using WeCare.Domain.Core.Dto;

namespace WeCare.Domain.Core.Service;

public interface IAuthService
{
    Task<LoginResponseDto> RegisterAsync(RegisterRequestDto dto);
    Task<LoginResponseDto> LoginAsync(LoginRequestDto dto);
    Task<TwoFactorResponseDto> VerifyTwoFactorAsync(TwoFactorRequestDto dto);
    Task<bool> EnableTwoFactorAsync(string userId);
    Task<bool> DisableTwoFactorAsync(string userId);
} 