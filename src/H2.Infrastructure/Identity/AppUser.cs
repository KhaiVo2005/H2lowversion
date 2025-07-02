using Microsoft.AspNetCore.Identity;

namespace H2_Infrastructure.Identity
{
    public class AppUser : IdentityUser
    {
        public string? FullName { get; set; }
        public string? DeviceId { get; set; }
    }
}
