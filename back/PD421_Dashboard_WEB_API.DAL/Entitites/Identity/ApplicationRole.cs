using Microsoft.AspNetCore.Identity;

namespace PD421_Dashboard_WEB_API.DAL.Entitites.Identity
{
    public class ApplicationRole : IdentityRole
    {
        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; } = [];
        public virtual ICollection<ApplicationRoleClaim> RoleClaims { get; set; } = [];
    }
}
