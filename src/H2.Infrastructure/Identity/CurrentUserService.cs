using H2.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace H2_Infrastructure.Identity
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }
        public string? UserId => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

        public string? FullName => _httpContextAccessor.HttpContext?.User?.Identity?.Name;

        public string? DeviceId => _httpContextAccessor.HttpContext?.User?.FindFirstValue("device_id");
    }
}
