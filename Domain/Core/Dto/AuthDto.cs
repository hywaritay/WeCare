namespace WeCare.Domain.Core.Dto;

public class RegisterRequestDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string? Role { get; set; } // "CommunityMother" or "AdminDoctor"
}

public class LoginRequestDto
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class LoginResponseDto
{
    public string Token { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public bool Is2FARequired { get; set; }
}

public class TwoFactorRequestDto
{
    public string UserId { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
}

public class TwoFactorResponseDto
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public string? Token { get; set; }
    public string? RefreshToken { get; set; }
} 