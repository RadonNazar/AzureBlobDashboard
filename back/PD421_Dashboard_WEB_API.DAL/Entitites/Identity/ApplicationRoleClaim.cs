using Microsoft.AspNetCore.Identity;

namespace PD421_Dashboard_WEB_API.DAL.Entitites.Identity
{
    public class ApplicationRoleClaim : IdentityRoleClaim<string>
    {
        public virtual ApplicationRole? Role { get; set; }
    }
}
