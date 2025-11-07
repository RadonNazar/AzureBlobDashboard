using Microsoft.AspNetCore.Identity;

namespace PD421_Dashboard_WEB_API.DAL.Entitites.Identity
{
    public class ApplicationUserRole : IdentityUserRole<string>
    {
        public virtual ApplicationUser? User { get; set; }
        public virtual ApplicationRole? Role { get; set; }
    }
}
