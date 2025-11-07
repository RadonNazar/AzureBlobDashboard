using Microsoft.AspNetCore.Identity;

namespace PD421_Dashboard_WEB_API.DAL.Entitites.Identity
{
    public class ApplicationUserToken : IdentityUserToken<string>
    {
        public virtual ApplicationUser? User { get; set; }
    }
}
