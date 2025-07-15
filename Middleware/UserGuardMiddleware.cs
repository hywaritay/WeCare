using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using WeCare.Domain.Infrastructure.Repository;

namespace WeCare.Middleware;

public class UserGuardMiddleware
{
    private readonly RequestDelegate _next;

    public UserGuardMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IUserRepository userRepository)
    {
        if (context.User.Identity?.IsAuthenticated == true)
        {
            var userIdClaim = context.User.FindFirst(ClaimTypes.NameIdentifier) ?? context.User.FindFirst(ClaimTypes.Name);
            if (userIdClaim != null && Guid.TryParse(userIdClaim.Value, out var userId))
            {
                var user = await userRepository.GetByIdAsync(userId);
                if (user == null || !user.IsActive)
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    await context.Response.WriteAsync("User is not active or does not exist.");
                    return;
                }
            }
        }
        await _next(context);
    }
}
