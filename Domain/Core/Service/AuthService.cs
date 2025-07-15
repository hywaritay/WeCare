using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WeCare.Domain.Core.Dto;
using WeCare.Domain.Infrastructure.Entity;
using WeCare.Domain.Infrastructure.Repository;
using System.Net.Mail;
using System.Net;

namespace WeCare.Domain.Core.Service;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly ITwoFactorAuthRepository _twoFactorAuthRepository;
    private readonly IConfiguration _config;

    public AuthService(IUserRepository userRepository, ITwoFactorAuthRepository twoFactorAuthRepository, IConfiguration config)
    {
        _userRepository = userRepository;
        _twoFactorAuthRepository = twoFactorAuthRepository;
        _config = config;
    }

    public async Task<LoginResponseDto> RegisterAsync(RegisterRequestDto dto)
    {
        var existing = await _userRepository.GetByEmailAsync(dto.Email);
        if (existing != null)
            throw new Exception("Email already registered");

        var user = new User
        {
            Id = Guid.NewGuid(),
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            Role = dto.Role == "AdminDoctor" ? UserRole.AdminDoctor : UserRole.CommunityMother,
            CreatedAt = DateTime.UtcNow,
            IsActive = true
        };
        await _userRepository.AddAsync(user);
        await _userRepository.SaveChangesAsync();
        return await LoginAsync(new LoginRequestDto { Email = dto.Email, Password = dto.Password });
    }

    public async Task<LoginResponseDto> LoginAsync(LoginRequestDto dto)
    {
        var user = await _userRepository.GetByEmailAsync(dto.Email);
        if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            throw new Exception("Invalid credentials");

        var is2FARequired = user.Role == UserRole.AdminDoctor;
        if (is2FARequired)
        {
            var otp = OtpHelper.GenerateOtp();
            var otpHash = OtpHelper.HashOtp(otp);
            var expiresAt = DateTime.UtcNow.AddMinutes(5);

            var twoFA = await _twoFactorAuthRepository.GetByUserIdAsync(user.Id);
            if (twoFA == null)
            {
                twoFA = new TwoFactorAuth
                {
                    Id = Guid.NewGuid(),
                    UserId = user.Id,
                    IsEnabled = true,
                    CreatedAt = DateTime.UtcNow
                };
                await _twoFactorAuthRepository.AddAsync(twoFA);
            }
            twoFA.OtpHash = otpHash;
            twoFA.OtpExpiresAt = expiresAt;
            await _twoFactorAuthRepository.SaveChangesAsync();

            // Send OTP via email
            await SendOtpEmailAsync(user.Email, otp);

            return new LoginResponseDto
            {
                UserId = user.Id.ToString(),
                Role = user.Role.ToString(),
                Is2FARequired = true
            };
        }
        // If not 2FA, return JWT
        var token = GenerateJwtToken(user);
        return new LoginResponseDto
        {
            Token = token,
            UserId = user.Id.ToString(),
            Role = user.Role.ToString(),
            Is2FARequired = false
        };
    }

    public async Task<TwoFactorResponseDto> VerifyTwoFactorAsync(TwoFactorRequestDto dto)
    {
        if (!Guid.TryParse(dto.UserId, out var userId))
            return new TwoFactorResponseDto { Success = false, Message = "Invalid user ID" };
        var twoFA = await _twoFactorAuthRepository.GetByUserIdAsync(userId);
        if (twoFA == null || !twoFA.IsEnabled)
            return new TwoFactorResponseDto { Success = false, Message = "2FA not enabled" };
        if (twoFA.OtpExpiresAt == null || twoFA.OtpExpiresAt < DateTime.UtcNow)
            return new TwoFactorResponseDto { Success = false, Message = "OTP expired" };
        if (!OtpHelper.VerifyOtp(dto.Code, twoFA.OtpHash))
            return new TwoFactorResponseDto { Success = false, Message = "Invalid OTP" };

        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
            return new TwoFactorResponseDto { Success = false, Message = "User not found" };

        // Optionally clear OTP after use
        twoFA.OtpHash = null;
        twoFA.OtpExpiresAt = null;
        await _twoFactorAuthRepository.SaveChangesAsync();

        var token = GenerateJwtToken(user);
        return new TwoFactorResponseDto { Success = true, Token = token };
    }

    public async Task<bool> EnableTwoFactorAsync(string userId)
    {
        if (!Guid.TryParse(userId, out var guid)) return false;
        var twoFA = await _twoFactorAuthRepository.GetByUserIdAsync(guid);
        if (twoFA == null) return false;
        twoFA.IsEnabled = true;
        await _twoFactorAuthRepository.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DisableTwoFactorAsync(string userId)
    {
        if (!Guid.TryParse(userId, out var guid)) return false;
        var twoFA = await _twoFactorAuthRepository.GetByUserIdAsync(guid);
        if (twoFA == null) return false;
        twoFA.IsEnabled = false;
        await _twoFactorAuthRepository.SaveChangesAsync();
        return true;
    }

    private string GenerateJwtToken(User user)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Email),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"] ?? "supersecretkey"));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: creds
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private async Task SendOtpEmailAsync(string email, string otp)
    {
        var smtpClient = new SmtpClient("smtp.gmail.com")
        {
            Port = 587,
            Credentials = new NetworkCredential("yayah.waritay@njala.edu.sl", "ezpmjqjkctgzrcap"),
            EnableSsl = true,
        };


        var mailMessage = new MailMessage
        {
            From = new MailAddress("yayah.waritay@njala.edu.sl"),
            Subject = "Your OTP Code",
            Body = $"Your OTP code is: {otp}",
            IsBodyHtml = false,
        };
        mailMessage.To.Add(email);
        await smtpClient.SendMailAsync(mailMessage);
    }
}

public static class OtpHelper
{
    public static string GenerateOtp(int length = 6)
    {
        var rng = new Random();
        return string.Concat(Enumerable.Range(0, length).Select(_ => rng.Next(0, 10).ToString()));
    }

    public static string HashOtp(string otp)
    {
        // Use a secure hash in production (e.g., BCrypt or SHA256)
        return BCrypt.Net.BCrypt.HashPassword(otp);
    }

    public static bool VerifyOtp(string otp, string hash)
    {
        return BCrypt.Net.BCrypt.Verify(otp, hash);
    }
} 